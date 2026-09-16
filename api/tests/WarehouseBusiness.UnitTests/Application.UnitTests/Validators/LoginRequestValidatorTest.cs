using Application.UnitTests.Services;
using FluentValidation;

namespace Application.UnitTests.Validators
{
  public class LoginRequestValidatorTest : AbstractValidator<ValidatableLoginRequest>
  {
    public LoginRequestValidatorTest()
    {
      RuleFor(x => x.Username).NotEmpty().MinimumLength(8).MaximumLength(20);
      RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(200);
    }
  }
}