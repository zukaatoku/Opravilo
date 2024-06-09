using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.Account;

public class RefreshTokenResponse
{
    [Required]
    public bool IsSuccess { get; set; }
}