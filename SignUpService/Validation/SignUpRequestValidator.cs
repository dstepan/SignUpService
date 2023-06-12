using FluentValidation;
using SignUpService.DTO;

namespace SignUpService.Validation;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(r => r.Email).NotEmpty().EmailAddress();
        RuleFor(r => r.Email).Must(r => !r.Any(char.IsWhiteSpace))
            .WithMessage("White spaces are not allowed.");
    }
}