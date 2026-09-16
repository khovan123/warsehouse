using Application.Interfaces;
using Application.Services;
using Application.UnitTests.Dtos;
using Application.UnitTests.Validators;
using Contract.Interfaces;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.Services
{
  public sealed record ValidatableLoginRequest : LoginTestDTO.Request, IFlagValidatableRequest { }

  public sealed class ValidationRunnerTest
  {
    private const string USERNAME_VALID = "username-valid";
    private const string USERNAME_INVALID = "u";
    private const string PASSWORD_VALID = "password-valid";
    private const string PASSSWORD_INVALID = "p";
    private const string USERNAME_PROPERTY = "Username";
    private const string PASSWORD_PROPERTY = "Password";
    private const string ERROR_MESSAGE = "invalid";

    [Fact]
    public async Task RunValidationAsync_WhenRequestIsNull_ShouldNotThrow()
    {
      var sp = Substitute.For<IServiceProvider>();

      var svc = new ValidationRunner(sp);

      var act = async () => await svc.RunValidationAsync<ValidatableLoginRequest>(null!, CancellationToken.None);

      await act.Should().NotThrowAsync();
      sp.DidNotReceive().GetService(Arg.Any<Type>());
    }

    [Fact]
    public async Task RunValidationAsync_WhenRequestIsNotValidatable_ShouldNotResolveValidators_AndShouldNotThrown()
    {
      var sp = Substitute.For<IServiceProvider>();
      var svc = new ValidationRunner(sp);

      var req = new LoginTestDTO.Request { Username = USERNAME_INVALID, Password = PASSSWORD_INVALID };

      var act = () => svc.RunValidationAsync(req, CancellationToken.None);

      await act.Should().NotThrowAsync();
      sp.DidNotReceive().GetService(Arg.Any<Type>());
    }

    [Fact]
    public async Task RunValidationAsync_WhenNoValidators_ShouldNotThrow()
    {
      var sp = Substitute.For<IServiceProvider>();
      var svc = new ValidationRunner(sp);

      var req = new ValidatableLoginRequest { Username = USERNAME_VALID, Password = PASSWORD_VALID };

      sp.GetService(typeof(IEnumerable<IValidator<ValidatableLoginRequest>>))
        .Returns(new List<IValidator<ValidatableLoginRequest>>(0));

      var act = async () => await svc.RunValidationAsync(req, CancellationToken.None);

      await act.Should().NotThrowAsync();
      sp.Received(1).GetService(typeof(IEnumerable<IValidator<ValidatableLoginRequest>>));
    }

    [Fact]
    public async Task RunValidationAsync_WhenValid_ShouldNotThrow()
    {
      var sp = Substitute.For<IServiceProvider>();
      var svc = new ValidationRunner(sp);

      var req = new ValidatableLoginRequest { Username = USERNAME_VALID, Password = PASSWORD_VALID };

      var validator = Substitute.For<IValidator<ValidatableLoginRequest>>();
      validator.ValidateAsync(req, Arg.Any<CancellationToken>())
        .Returns(Task.FromResult(new ValidationResult(new ValidationFailure[0])));

      sp.GetService(typeof(IEnumerable<IValidator<ValidatableLoginRequest>>))
        .Returns(new List<IValidator<ValidatableLoginRequest>> { validator });

      var act = async () => await svc.RunValidationAsync(req, CancellationToken.None);

      await act.Should().NotThrowAsync();
      await validator.Received(1).ValidateAsync(req, Arg.Any<CancellationToken>());
      sp.Received(1).GetService(typeof(IEnumerable<IValidator<ValidatableLoginRequest>>));
    }

    [Fact]
    public async Task RunValidationAsync_WhenInvalid_ShouldThrow()
    {
      var sp = Substitute.For<IServiceProvider>();
      var svc = new ValidationRunner(sp);

      var req = new ValidatableLoginRequest { Username = USERNAME_INVALID, Password = PASSSWORD_INVALID };

      var validator = Substitute.For<IValidator<ValidatableLoginRequest>>();

      validator.ValidateAsync(req, Arg.Any<CancellationToken>())
        .Returns(Task.FromResult(new ValidationResult(new[]
          {
            new ValidationFailure(USERNAME_PROPERTY, ERROR_MESSAGE),
            new ValidationFailure(PASSWORD_PROPERTY, ERROR_MESSAGE)
          }
        )));

      sp.GetService(typeof(IEnumerable<IValidator<ValidatableLoginRequest>>))
        .Returns(new List<IValidator<ValidatableLoginRequest>> { validator });

      Func<Task> act = () => svc.RunValidationAsync(req, CancellationToken.None);

      var ex = await act.Should().ThrowAsync<ValidationException>();
      ex.Which.Errors.Should().Contain(e => e.PropertyName == USERNAME_PROPERTY && e.ErrorMessage == ERROR_MESSAGE);
      ex.Which.Errors.Should().Contain(e => e.PropertyName == PASSWORD_PROPERTY && e.ErrorMessage == ERROR_MESSAGE);
      sp.Received(1).GetService(typeof(IEnumerable<IValidator<ValidatableLoginRequest>>));
    }

    [Fact]
    public async Task LoginRequestValidator_WhenValid_ShouldPass()
    {
      var v = new LoginRequestValidatorTest();
      var req = new ValidatableLoginRequest
      {
        Username = USERNAME_VALID,
        Password = PASSWORD_VALID
      };

      var result = await v.ValidateAsync(req);

      result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task LoginRequestValidator_WhenValid_ShouldNotPass()
    {
      var v = new LoginRequestValidatorTest();
      var req = new ValidatableLoginRequest
      {
        Username = USERNAME_INVALID,
        Password = PASSSWORD_INVALID
      };

      var result = await v.ValidateAsync(req);

      result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task LoginRequestValidator_WhenValid_ShouldPass_WithRealValidator()
    {
      var services = new ServiceCollection();
      services.AddSingleton<IValidationRunner, ValidationRunner>();
      var sp = services.BuildServiceProvider();
      var svc = new ValidationRunner(sp);

      var req = new ValidatableLoginRequest
      {
        Username = USERNAME_VALID,
        Password = PASSWORD_VALID
      };

      var act = async () => await svc.RunValidationAsync(req, CancellationToken.None);

      await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task LoginRequestValidator_WhenValid_ShouldNotPass_WithRealValidator()
    {
      var services = new ServiceCollection();
      services.AddSingleton<IValidationRunner, ValidationRunner>();
      var sp = services.BuildServiceProvider();

      var svc = new ValidationRunner(sp);

      var req = new ValidatableLoginRequest
      {
        Username = USERNAME_INVALID,
        Password = PASSSWORD_INVALID
      };

      var act = async () => await svc.RunValidationAsync(req, CancellationToken.None);

      await act.Should().NotThrowAsync();
    }

  }
}