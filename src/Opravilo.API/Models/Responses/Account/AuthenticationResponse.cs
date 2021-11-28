using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.Account
{
    public class AuthenticationResponse
    {
        [Required]
        public string Token { get; set; }
        
        [Required]
        public bool IsSuccess { get; set; }
        
        public List<string> Errors { get; set; }
    }
}