using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.Project
{
    public class ProjectResponse
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}