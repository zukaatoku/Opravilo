using System.Collections.Generic;

namespace Opravilo.Application.Models.Project
{
    public class FullStateModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<CardModel> Cards { get; set; }
    }
}