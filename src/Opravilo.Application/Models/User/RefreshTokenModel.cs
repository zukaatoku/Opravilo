using System;

namespace Opravilo.Application.Models.User
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}