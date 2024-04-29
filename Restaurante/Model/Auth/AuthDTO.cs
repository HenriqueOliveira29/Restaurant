namespace Restaurant.Infraestructure.Model.Auth
{
    public class AuthDTO
    {
        public string? Username { get; set; }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        public string[]? Roles { get; set; }
        public string RecaptchaToken { get; set; }
        public string? PreferedView { get; set; }
    }
}
