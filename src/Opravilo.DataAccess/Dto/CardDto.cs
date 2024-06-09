namespace Opravilo.DataAccess.Dto
{
    public record CardDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}