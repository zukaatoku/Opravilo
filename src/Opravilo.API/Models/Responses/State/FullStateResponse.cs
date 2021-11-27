using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Opravilo.API.Models.Responses.Card;

namespace Opravilo.API.Models.Responses.State
{
    public class FullStateResponse
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public List<CardResponse> Cards { get; set; }
    }
}