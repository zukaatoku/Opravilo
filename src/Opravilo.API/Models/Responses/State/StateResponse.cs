using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.State
{
    public class StateResponse
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}