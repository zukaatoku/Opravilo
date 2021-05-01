namespace Opravilo.API.Models.Requests
{
    public class RegistrationRequest
    {
        public string Login { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
    }
}