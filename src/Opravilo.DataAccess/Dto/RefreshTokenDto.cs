using System;

namespace Opravilo.DataAccess.Dto
{
    public record RefreshTokenDto
    {
        public DateTime ExpirationDate { get; init; }
        public string RefreshToken { get; init; }
    }
}