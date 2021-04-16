using Opravilo.Application.Interfaces.Services;
using Opravilo.Application.Models.User;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserModel FindUser(string login, string password)
        {
            var user = _userRepository.FindUser(login, password);

            if (user == null)
            {
                return null;
            }

            return new UserModel()
            {
                Login = user.Login,
            };
        }
    }
}