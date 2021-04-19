using System;

namespace Opravilo.DataAccess.Dto
{
    public class RefreshTokenDto
    {
        public DateTime ExpirationDate { get; set; }
        public string RefreshToken { get; set; }
    }
}