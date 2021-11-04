namespace Opravilo.Application.Models.Requests
{
    public class UpdateProjectRequest
    {
        public long ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}