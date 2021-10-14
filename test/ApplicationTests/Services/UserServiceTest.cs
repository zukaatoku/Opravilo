using System;
using Moq;
using Opravilo.Application.Services;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.Repositories;
using Xunit;

namespace ApplicationTests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
        }

        [Fact]
        public void SaveRefreshTokens_ShouldCall_Repository()
        {
            const int expectedUserId = 1;
            var expectedDateTime = DateTime.Now;
            const string expectedToken = "refresh_token";
            
            _refreshTokenRepositoryMock.Setup(r => r.SaveRefreshToken(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);

            service.SaveRefreshToken(expectedUserId, expectedToken, expectedDateTime);
            
            _refreshTokenRepositoryMock.Verify(r => r.SaveRefreshToken(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Exactly(1));
        }
        
        [Fact]
        public void CleanRefreshTokens_ShouldCall_Repository()
        {
            _refreshTokenRepositoryMock.Setup(r => r.CleanRefreshTokens(It.IsAny<long>()));

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);

            service.CleanRefreshTokens(0);
            _refreshTokenRepositoryMock.Verify(r => r.CleanRefreshTokens(It.IsAny<long>()), Times.Exactly(1));
        }
        
        [Fact]
        public void FindToken_Should_Return_Model_When_There_Is_Token()
        {
            var expectedDateTime = DateTime.Now;
            var expectedToken = "refresh_token";
            
            _refreshTokenRepositoryMock.Setup(r => r.FindRefreshToken(It.IsAny<long>()))
                .Returns((long userId) => new RefreshTokenDto()
                {
                    ExpirationDate = expectedDateTime,
                    RefreshToken = expectedToken
                });

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);
            
            var token = service.FindToken(0);

            Assert.NotNull(token);
            Assert.Equal(expectedDateTime, token.ExpirationDate);
            Assert.Equal(expectedToken, token.RefreshToken);
        }

        [Fact]
        public void FindToken_Should_Return_Null_When_No_Token()
        {
            _refreshTokenRepositoryMock.Setup(r => r.FindRefreshToken(It.IsAny<long>()))
                .Returns(() => null);

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);
            
            var token = service.FindToken(0);
            Assert.Null(token);
        }

        [Fact]
        public void RegisterUser_Should_ReturnSuccess_WhenAllGood()
        {
            const long expectedId = 1;
            const string expectedLogin = "test";
            const string fakePassword = "123";
            const string expectedDisplayName = "display_name";
            
            _userRepositoryMock.Setup(r => r.LoginAvailable(It.IsAny<string>()))
                .Returns(true);
            
            _userRepositoryMock.Setup(r => r.AddUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string displayName, string password) => new UserDto()
                {
                    DisplayName = expectedDisplayName,
                    Id = expectedId,
                });

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);

            var result = service.RegisterUser(expectedLogin, expectedDisplayName, fakePassword);

            Assert.True(result.IsSuccess);
            Assert.Null(result.Errors);
            Assert.NotNull(result.CreatedUser);
            Assert.Equal(expectedDisplayName, result.CreatedUser.DisplayName);
        }

        [Fact]
        public void RegisterUser_Should_ReturnError_WhenInvalidCredentials()
        {
            _userRepositoryMock.Setup(r => r.LoginAvailable(It.IsAny<string>()))
                .Returns(false);

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);

            var registrationResult = service.RegisterUser("any login", "any_display_name", "any password");
            
            Assert.False(registrationResult.IsSuccess);
            Assert.NotEmpty(registrationResult.Errors);
            Assert.Null(registrationResult.CreatedUser);
        }

        [Fact]
        public void FindUser_Should_Return_Model_When_There_Is_User()
        {
            const string expectedLogin = "test";
            const string expectedDisplayName = "display_name";
            const long expectedId = 1;
            _userRepositoryMock.Setup(r => r.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string pwd) => new UserDto()
                {
                    Id = expectedId,
                    DisplayName = expectedDisplayName,
                });

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);

            var user = service.FindUser(expectedLogin, "test");

            Assert.NotNull(user);
            Assert.Equal(expectedDisplayName, user.DisplayName);
            Assert.Equal(expectedId, user.Id);
            Assert.NotEqual(0, user.Id);
        }

        [Fact]
        public void FindUser_Should_Return_Null_When_No_User()
        {
            _userRepositoryMock.Setup(r => r.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => null);

            var service = new UserService(_userRepositoryMock.Object, _refreshTokenRepositoryMock.Object);

            var user = service.FindUser("test", "test2");
            Assert.Null(user);
        }
    }
}