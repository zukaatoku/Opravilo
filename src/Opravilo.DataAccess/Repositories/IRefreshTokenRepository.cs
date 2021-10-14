using System;
using Opravilo.DataAccess.Dto;

namespace Opravilo.DataAccess.Repositories
{
    public interface IRefreshTokenRepository
    {
        void SaveRefreshToken(long userId, string refreshToken, DateTime expirationTime);
        void CleanRefreshTokens(long userId);
        RefreshTokenDto FindRefreshToken(long userId);
    }
}