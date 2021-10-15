using System;
using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IUserRepository
    {
        UserDto FindUser(string login, string passwordHash);
        UserDto FindUser(string vkId);
        UserDto AddUser(string login, string displayName, string passwordHash);
        UserDto AddVkUser(string firstName, string surname, string vkId);
        bool LoginAvailable(string login);
    }
}