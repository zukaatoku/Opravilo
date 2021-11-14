namespace Opravilo.Application.Models.Requests
{
    public class UpdateStateRequest
    {
        public long ProjectId { get; set; }
        public long StateId { get; set; }
        public string Name { get; set; }
    }
}