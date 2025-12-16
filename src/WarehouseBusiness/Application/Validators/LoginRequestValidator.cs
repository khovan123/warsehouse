using Application.DTOs;
using FluentValidation;

namespace API.Validators;

public class LoginRequestValidator : AbstractValidator<LoginDTO.Request>
{
  public LoginRequestValidator()
  {
    RuleFor(x => x.Username).NotEmpty().MinimumLength(8).MaximumLength(20);
    RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(200);
  }
}
