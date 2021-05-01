using System;
using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IUserRepository
    {
        UserDto FindUser(string login, string passwordHash);
        UserDto FindUser(string vkId);
        UserDto AddUser(string login, string displayName, string passwordHash);
        void SaveRefreshToken(long userId, string refreshToken, DateTime expirationTime);
        RefreshTokenDto FindRefreshToken(long userId);
        void CleanRefreshTokens(long userId);
        bool LoginAvailable(string login);
        bool VkIdAvailable(string vkId);
    }
}