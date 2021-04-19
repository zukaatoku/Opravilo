namespace Opravilo.DataAccess.Dto
{
    public record UserDto
    {
        public long Id { get; init; }
        public string Login { get; init; }
    }
}