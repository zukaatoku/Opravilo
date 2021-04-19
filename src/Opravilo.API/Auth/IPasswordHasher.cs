namespace Opravilo.API.Auth
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}