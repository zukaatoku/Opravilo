using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IUserRepository
    {
        UserDto FindUser(string login, string password);
    }
}