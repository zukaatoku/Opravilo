using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Opravilo.API.Models.Responses.State;
using Opravilo.API.Models.Responses.User;

namespace Opravilo.API.Models.Responses.Project
{
    public class FullProjectResponse
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public UserResponse Creator { get; set; }
        
        public List<FullStateResponse> States { get; set; }
    }
}