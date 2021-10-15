using System.Collections.Generic;

namespace Opravilo.API.Models.Responses
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}