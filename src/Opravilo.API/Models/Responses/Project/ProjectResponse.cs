using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.Project
{
    public class ProjectResponse
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}