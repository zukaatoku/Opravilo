using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.User
{
    public class UserResponse
    {
        [Required]
        public long Id { get; init; }
        
        [Required]
        public string DisplayName { get; init; }
    }
}