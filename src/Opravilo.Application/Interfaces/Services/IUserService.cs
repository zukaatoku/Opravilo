using Opravilo.Application.Models.User;

namespace Opravilo.Application.Interfaces.Services
{
    public interface IUserService
    {
        UserModel FindUser(string login, string password);
    }
}