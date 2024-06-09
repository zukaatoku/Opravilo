using System;
using System.Collections.Generic;
using ApiUnitTests.Helpers;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Opravilo.API.Auth;
using Opravilo.API.Models.Auth;
using Opravilo.API.Models.Responses;
using Opravilo.API.Options;
using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.User;
using Xunit;

namespace ApiUnitTests.Auth
{
    public class AuthManagerTests
    {
        private const int FakeJwtLifetime = 1;
        
        private readonly Mock<IUserService> _serviceMock;
        private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
        private readonly Mock<ITokenValidationParametersCreator> _tokenParametersCreatorMock;
        
        private UserManager _authManager;
        private JwtAuthOptions _authOptions;
        
        public AuthManagerTests()
        {
            _serviceMock = new Mock<IUserService>();
            
            _tokenGeneratorMock = new Mock<ITokenGenerator>();
            _tokenGeneratorMock.Setup(g => g.GetToken(It.IsAny<string>(), It.IsAny<long>()))
                .Returns("fake_jwt_token");
            _tokenGeneratorMock.Setup(g => g.GetRefreshToken())
                .Returns("fake_refresh_token");

            _tokenParametersCreatorMock = new Mock<ITokenValidationParametersCreator>();
            _tokenParametersCreatorMock.Setup(t => t.Create(It.IsAny<JwtAuthOptions>(), It.IsAny<bool>()))
                .Returns(() => new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _authOptions.Audience,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = _authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                });

            _authOptions = new JwtAuthOptions()
            {
                Issuer = "fake_issuer",
                Audience = "fake_audience",
                Key = "fake_key_fake_key_fake_key",
                RefreshLifetime = 5,
                Lifetime = FakeJwtLifetime,
            };
        }
        
        [Fact]
        public void Register_Should_ReturnSuccess_WhenServiceReturnsUser()
        {
            _serviceMock.Setup(s => s.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string displayName, string password) => new RegistrationResultModel() 
                { 
                    CreatedUser = new UserModel()
                    {
                        Id = 1,
                        DisplayName = displayName,
                    }
                });

            _authManager = new UserManager(_serviceMock.Object, _tokenGeneratorMock.Object, null, _authOptions);

            var result = _authManager.Register("Login", "displayLogin", "Password");

            AssertSuccess(result);
        }

        [Fact]
        public void Register_Should_ReturnFalse_WhenServiceReturnsNotSuccess()
        {
            const string expectedError = "error";
            _serviceMock.Setup(s => s.RegisterUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string displayName, string password) => new RegistrationResultModel()
                {
                    Errors = new List<string>()
                    {
                        expectedError
                    }
                });

            _authManager = new UserManager(_serviceMock.Object, _tokenGeneratorMock.Object, null, _authOptions);

            var result = _authManager.Register("Login", "displayLogin", "Password");

            AssertFail(result, expectedError);
        }
        
        [Fact]
        public void Authenticate_Should_ReturnSuccess_WhenUserIsInDb()
        {
            _serviceMock.Setup(s => s.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string password) => new UserModel()
                {
                    Id = 1,
                    DisplayName = "DisplayName"
                });

            _authManager = new UserManager(_serviceMock.Object, _tokenGeneratorMock.Object, null, _authOptions);

            var result = _authManager.Authenticate("Login", "Password");

            AssertSuccess(result);
        }

        [Fact]
        public void Authenticate_Should_ReturnFalse_WhenUserNotFound()
        {
            _serviceMock.Setup(s => s.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string password) => null);

            _authManager = new UserManager(_serviceMock.Object, _tokenGeneratorMock.Object, null, _authOptions);

            var result = _authManager.Authenticate("Login", "Password");

            AssertFail(result, "Failed to find user!");
        }
        
        [Fact]
        public void RefreshToken_Should_ReturnSuccess_WhenAllGood()
        {
            const string fakeRefresh = "fake_refresh_token";
            var now = DateTime.Now;
            var userService = SetupUserServiceFindToken(fakeRefresh, now, 1);
            
            var fakeJwt = FakeTokenGenerator.GetJwtToken(_authOptions, now.AddMinutes(FakeJwtLifetime - 2), "fake_login", 1);

            _authManager = new UserManager(userService, _tokenGeneratorMock.Object, _tokenParametersCreatorMock.Object,
                _authOptions);

            var result = _authManager.RefreshToken(fakeRefresh);
            
            AssertSuccess(result);
        }

        [Fact]
        public void RefreshToken_Should_ReturnFalse_WhenJwtTokenNotExpired()
        {
            const string fakeRefresh = "fake_refresh_token";
            var now = DateTime.Now;
            var userService = SetupUserServiceFindToken(fakeRefresh, now, 1);
            
            var fakeJwt = FakeTokenGenerator.GetJwtToken(_authOptions, now, "fake_login", 1);

            _authManager = new UserManager(userService, _tokenGeneratorMock.Object, _tokenParametersCreatorMock.Object,
                _authOptions);

            var result = _authManager.RefreshToken(fakeRefresh);
            
            AssertFail(result, "JWT token not expired yet!");
        }

        [Fact]
        public void RefreshToken_Should_ReturnFalse_WhenRefreshTokenNotFoundInDb()
        {
            const string fakeRefresh = "fake_refresh_token";
            var now = DateTime.Now;
            var userService = SetupUserServiceFindToken("different_refresh_token", now, 1);
            
            var fakeJwt = FakeTokenGenerator.GetJwtToken(_authOptions, now.AddMinutes(-2), "fake_login", 1);

            _authManager = new UserManager(userService, _tokenGeneratorMock.Object, _tokenParametersCreatorMock.Object,
                _authOptions);

            var result = _authManager.RefreshToken(fakeRefresh);
            
            AssertFail(result, "Refresh token invalid!");
        }

        [Fact]
        public void RefreshToken_Should_ReturnFalse_WhenRefreshTokenExpired()
        {
            const string fakeRefresh = "fake_refresh_token";
            var now = DateTime.Now;
            var userService = SetupUserServiceFindToken(fakeRefresh, now, -1);
            
            var fakeJwt = FakeTokenGenerator.GetJwtToken(_authOptions, now.AddDays(-5), "fake_login", 1);

            _authManager = new UserManager(userService, _tokenGeneratorMock.Object, _tokenParametersCreatorMock.Object,
                _authOptions);

            var result = _authManager.RefreshToken(fakeRefresh);
            
            AssertFail(result, "Refresh token expired!");
        }

        private IUserService SetupUserServiceFindToken(string refreshToken, DateTime date, int dateIncrement)
        {
            _serviceMock.Setup(s => s.FindToken(It.IsAny<long>()))
                .Returns(() => new RefreshTokenModel()
                {
                    RefreshToken = refreshToken,
                    ExpirationDate = date.AddDays(dateIncrement)
                });

            return _serviceMock.Object;
        }

        private void AssertSuccess(AuthenticationResult result)
        {
            Assert.True(result.IsSuccess);
            Assert.NotEmpty(result.Token);
            Assert.NotEmpty(result.RefreshToken);
            Assert.Null(result.Errors);
        }
        
        private void AssertFail(AuthenticationResult result, string message)
        {
            Assert.False(result.IsSuccess);
            Assert.True(string.IsNullOrEmpty(result.Token));
            Assert.True(string.IsNullOrEmpty(result.RefreshToken));
            Assert.NotEmpty(result.Errors);
            Assert.Equal(message, result.Errors[0]);
        }
    }
}