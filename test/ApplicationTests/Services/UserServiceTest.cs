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
        public void FindUser_Should_Return_Model_When_There_Is_User()
        {
            _repository.Setup(r => r.FindUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string login, string pwd) => new UserDto()
                {
                    Login = login,
                });

            _service = new UserService(_repository.Object);

            var expectedLogin = "test";

            var user = _service.FindUser(expectedLogin, "test");

            Assert.NotNull(user);
            Assert.Equal(expectedLogin, user.Login);
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