using Application.Dtos;
using FluentValidation;

namespace Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginDTO.Request>
{
  public LoginRequestValidator()
  {
    RuleFor(x => x.Username).NotEmpty().MinimumLength(8).MaximumLength(20);
    RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(200);
  }
}
