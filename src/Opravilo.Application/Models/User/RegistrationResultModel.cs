using System.Collections.Generic;

namespace Opravilo.Application.Models.User
{
    public class RegistrationResultModel
    {
        public bool IsSuccess => !(Errors?.Count > 0);
        public List<string> Errors { get; set; }
        public UserModel CreatedUser { get; set; }
    }
}