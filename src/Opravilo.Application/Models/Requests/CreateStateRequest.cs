namespace Opravilo.Application.Models.Requests
{
    public class CreateStateRequest
    {
        public long ProjectId { get; set; }
        public string Name { get; set; }
    }
}