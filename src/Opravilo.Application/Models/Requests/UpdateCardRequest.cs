namespace Opravilo.Application.Models.Requests
{
    public class UpdateCardRequest
    {
        public long ProjectId { get; set; }
        public long CardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}