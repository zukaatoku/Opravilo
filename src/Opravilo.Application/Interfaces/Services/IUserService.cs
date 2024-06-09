using System;
using Opravilo.Application.Models.User;

namespace Opravilo.Application.Interfaces.Services
{
    public interface IUserService
    {
        UserModel FindUser(string vkId);
        UserModel FindUser(string login, string password);
        UserModel CreateVkUser(string givenName, string surname, string vkId);
        
        RegistrationResultModel RegisterUser(string login, string displayName, string passwordHash);
        
        RefreshTokenModel FindToken(long userId);
        RefreshTokenModel FindToken(string refreshToken);
        
        void SaveRefreshToken(long userId, string refreshToken, DateTime expirationTime);
        void CleanRefreshTokens(long userId);
    }
}