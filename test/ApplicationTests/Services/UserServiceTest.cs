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
        private readonly Mock<IUserRepository> _repository;
        private UserService _service;

        public UserServiceTest()
        {
            _repository = new Mock<IUserRepository>();
        }

        [Fact]
        public void SaveRefreshTokens_ShouldCall_Repository()
        {
            const int expectedUserId = 1;
            var expectedDateTime = DateTime.Now;
            const string expectedToken = "refresh_token";
            
            _repository.Setup(r => r.SaveRefreshToken(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<DateTime>()));

            _service = new UserService(_repository.Object);

            _service.SaveRefreshToken(expectedUserId, expectedToken, expectedDateTime);
            
            _repository.Verify(r => r.SaveRefreshToken(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Exactly(1));
        }
        
        [Fact]
        public void CleanRefreshTokens_ShouldCall_Repository()
        {
            _repository.Setup(r => r.CleanRefreshTokens(It.IsAny<long>()));

            _service = new UserService(_repository.Object);

            _service.CleanRefreshTokens(0);
            
            _repository.Verify(r => r.CleanRefreshTokens(It.IsAny<long>()), Times.Exactly(1));
        }
        
        [Fact]
        public void FindToken_Should_Return_Model_When_There_Is_Token()
        {
            var expectedDateTime = DateTime.Now;
            var expectedToken = "refresh_token";
            
            _repository.Setup(r => r.FindRefreshToken(It.IsAny<long>()))
                .Returns((long userId) => new RefreshTokenDto()
                {
                    ExpirationDate = expectedDateTime,
                    RefreshToken = expectedToken
                });

            _service = new UserService(_repository.Object);

            var token = _service.FindToken(0);

            Assert.NotNull(token);
            Assert.Equal(expectedDateTime, token.ExpirationDate);
            Assert.Equal(expectedToken, token.RefreshToken);
        }

        [Fact]
        public void FindToken_Should_Return_Null_When_No_Token()
        {
            _repository.Setup(r => r.FindRefreshToken(It.IsAny<long>()))
                .Returns(() => null);

            _service = new UserService(_repository.Object);

            var token = _service.FindToken(0);
            Assert.Null(token);
        }

        [Fact]
        public void RegisterUser_Should_Return_NewUser()
        {
            const long expectedId = 1;
            const string expectedLogin = "test";
            const string fakePassword = "123";
            
            _repository.Setup(r => r.AddUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string password) => new UserDto()
                {
                    Login = login,
                    Id = expectedId
                });

            _service = new UserService(_repository.Object);

            var user = _service.RegisterUser(expectedLogin, fakePassword);

            Assert.NotNull(user);
            Assert.Equal(expectedId, user.Id);
            Assert.Equal(expectedLogin, user.Login);
        }

        [Fact]
        public void FindUser_Should_Return_Model_When_There_Is_User()
        {
            const string expectedLogin = "test";
            const long expectedId = 1;
            _repository.Setup(r => r.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string pwd) => new UserDto()
                {
                    Id = expectedId,
                    Login = login,
                });

            _service = new UserService(_repository.Object);

            var user = _service.FindUser(expectedLogin, "test");

            Assert.NotNull(user);
            Assert.Equal(expectedLogin, user.Login);
            Assert.Equal(expectedId, user.Id);
            Assert.NotEqual(0, user.Id);
        }

        [Fact]
        public void FindUser_Should_Return_Null_When_No_User()
        {
            _repository.Setup(r => r.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() => null);

            _service = new UserService(_repository.Object);

            var user = _service.FindUser("test", "test2");
            Assert.Null(user);
        }
    }
}