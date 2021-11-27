namespace Opravilo.Application.Models.Requests
{
    public class CreateCardRequest
    {
        public long ProjectId { get; set; }
        public long StateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}