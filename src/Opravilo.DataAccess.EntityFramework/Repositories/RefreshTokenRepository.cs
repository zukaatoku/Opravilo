using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Opravilo.DataAccess.Dto;
using Opravilo.DataAccess.EntityFramework.Models;
using Opravilo.DataAccess.Repositories;

namespace Opravilo.DataAccess.EntityFramework.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly DataContext _context;

        public RefreshTokenRepository(DataContext context)
        {
            _context = context;
        }

        public void SaveRefreshToken(long userId, string refreshToken, DateTime expirationTime)
        {
            var now = DateTime.Now;

            var tokenModel = new RefreshTokenModel()
            {
                UserId = userId,
                RefreshToken = refreshToken,
                ExpirationDate = expirationTime,
                ChangedDate = now,
                CreatedDate = now
            };

            _context.RefreshTokens.Add(tokenModel);
            _context.SaveChanges();
        }

        public RefreshTokenDto FindRefreshToken(long userId)
        {
            var user = _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefault(u => u.Id == userId);

            var token = user.RefreshTokens.FirstOrDefault();

            if (token == null)
            {
                return null;
            }

            return new RefreshTokenDto()
            {
                ExpirationDate = token.ExpirationDate,
                RefreshToken = token.RefreshToken
            };
        }

        public void CleanRefreshTokens(long userId)
        {
            var tokens = _context.RefreshTokens.Where(r => r.UserId == userId).ToList();
            _context.RemoveRange(tokens);
            _context.SaveChanges();
        }
    }
}