using FluentValidation;

namespace Restaurant.Infraestructure.Model.Auth
{
    public class LoginDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RecaptchaToken { get; set; }
    }

    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email deve estar corretamente preenchido!");

            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password deve estar preenchida!");
        }
    }
}
