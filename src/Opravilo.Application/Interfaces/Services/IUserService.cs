using System;
using Opravilo.Application.Models.User;

namespace Opravilo.Application.Interfaces.Services
{
    public interface IUserService
    {
        UserModel FindUser(string login, string password);
        UserModel RegisterUser(string login, string passwordHash);
        void SaveRefreshToken(long userId, string refreshToken, DateTime expirationTime);
        RefreshTokenModel FindToken(long userId);
        void CleanRefreshTokens(long userId);
    }
}