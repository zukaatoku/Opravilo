using System.Collections.Generic;

namespace Opravilo.DataAccess.Dto
{
    public class FullStateDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<CardDto> Cards { get; set; }
    }
}