using System.ComponentModel.DataAnnotations;

namespace Opravilo.API.Models.Responses.Card
{
    public class CardResponse
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}