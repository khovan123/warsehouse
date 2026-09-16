using Application.Dtos;
using Application.Exceptions;
using Application.Helper;
using Application.Helper.Options;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.Services
{
  public sealed class AuthServiceTests
  {
    private const string USER_ID = "userId";
    private const string USERNAME = "username";
    private const string EMAIL = "email@test.com";
    private const string PASSWORD = "password";
    private const string WRONG_USERNAME = "no-username";
    private const string WRONG_PASSWORD = "wrong-password";
    private const string SECRETKEY = "super-secret-key-super-secret-key";
    private const string ISSUER = "test-issuer";
    private const string AUDIENCE = "test-audience";
    private const int TOKENEXPIRED = 15;
    private const string USERNAME_FAILED_VALIDATION = "u";
    private const string PASSWORD_FAILED_VALIDATION = "u";
    private const string REFRESH_TOKEN = "refresh-token";
    private const string ERROR_MESSAGE_VALIDATION_FAILED = "validation failed";

    private static IOptions<JwtOptions> GenerateJwtOptions() => Options.Create(new JwtOptions
    {
      SecretKey = SECRETKEY,
      Issuer = ISSUER,
      Audience = AUDIENCE,
      TokenExpiresMinutes = TOKENEXPIRED
    });

    [Fact]
    public async Task LoginAsync_WhenUserNotFound_ShouldThrowUnauthorized()
    {
      var userRepo = Substitute.For<IUserRepository>();
      userRepo.GetByUsernameAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns((User?)null);

      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();
      validation.RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var act = async () => await svc.LoginAsync(new LoginDTO.Request
      {
        Username = USERNAME,
        Password = PASSWORD
      }, CancellationToken.None);

      await act.Should().ThrowAsync<UnauthorizedException>();
      await validation.Received(1).RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>());
      await userRepo.Received(1).ReceivedWithAnyArgs().GetByUsernameAsync(default!, default);
      await refreshRepo.DidNotReceiveWithAnyArgs().CreateOneAsync(default!, default);
    }

    [Fact]
    public async Task LoginAsync_WhenPasswordInvalid_ShouldThrowUnauthorized()
    {
      var userRepo = Substitute.For<IUserRepository>();
      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();
      validation.RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);

      var user = new User
      {
        Id = USER_ID,
        Username = USERNAME,
        Email = EMAIL,
        Password = BCrypt.Net.BCrypt.HashPassword(PASSWORD)
      };

      userRepo.GetByUsernameAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns(user);

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var act = async () => await svc.LoginAsync(new LoginDTO.Request
      {
        Username = PASSWORD,
        Password = WRONG_PASSWORD
      }, CancellationToken.None);

      await act.Should().ThrowAsync<UnauthorizedException>();
      await validation.Received(1).RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>());
      await userRepo.Received(1).ReceivedWithAnyArgs().GetByUsernameAsync(default!, default);
      await refreshRepo.DidNotReceiveWithAnyArgs().CreateOneAsync(default!, default);
    }


    [Fact]
    public async Task LoginAsync_WhenValidateFailed_ShouldThrownValidationException()
    {
      var userRepo = Substitute.For<IUserRepository>();
      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();
      validation.RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>())
        .Returns(x => throw new ValidationException(ERROR_MESSAGE_VALIDATION_FAILED));

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var act = async () => await svc.LoginAsync(new LoginDTO.Request
      {
        Username = USERNAME_FAILED_VALIDATION,
        Password = PASSWORD_FAILED_VALIDATION
      }, CancellationToken.None);

      await act.Should().ThrowAsync<ValidationException>().WithMessage(ERROR_MESSAGE_VALIDATION_FAILED);
      await validation.Received(1).RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>());
      await userRepo.DidNotReceiveWithAnyArgs().GetByUsernameAsync(default!, default);
      await refreshRepo.DidNotReceiveWithAnyArgs().CreateOneAsync(default!, default);
    }

    [Fact]
    public async Task LoginAsync_WhenValid_ShouldReturnToken_AndStoreRefreshToken()
    {
      var userRepo = Substitute.For<IUserRepository>();
      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();
      validation.RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);

      var user = new User
      {
        Id = USER_ID,
        Username = USERNAME,
        Email = EMAIL,
        Password = BCrypt.Net.BCrypt.HashPassword(PASSWORD)
      };

      userRepo.GetByUsernameAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns(user);

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var result = await svc.LoginAsync(new LoginDTO.Request
      {
        Username = USERNAME,
        Password = PASSWORD
      }, CancellationToken.None);

      result.Should().NotBeNull();
      result.Result.Data.Should().NotBeNull();
      result.Result.Data.Response.Token.Should().NotBeNullOrWhiteSpace();
      result.Result.Data.RefreshToken.Should().NotBeNullOrWhiteSpace();

      await validation.Received(1).RunValidationAsync(Arg.Any<LoginDTO.Request>(), Arg.Any<CancellationToken>());
      await userRepo.Received(1).ReceivedWithAnyArgs().GetByUsernameAsync(default!, default);
      await refreshRepo.Received(1).CreateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>());

    }

    [Fact]
    public async Task RefreshAccessTokenAsync_WhenRefreshTokenNotFound_ShouldThrownInvalidRefreshToken()
    {
      var userRepo = Substitute.For<IUserRepository>();
      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();

      var now = DateTime.UtcNow;

      refreshRepo.GetByTokenHashAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns((RefreshToken?)null);
      refreshRepo.UpdateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);
      refreshRepo.CreateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var act = async () => await svc.RefreshAccessTokenAsync(default!, CancellationToken.None);

      await act.Should().ThrowAsync<InvalidRefreshTokenException>();
      await refreshRepo.Received(1).ReceivedWithAnyArgs().GetByTokenHashAsync(default!, default);
      await refreshRepo.DidNotReceive().UpdateOneAsync(default!, default);
      await refreshRepo.DidNotReceive().CreateOneAsync(default!, default);
    }

    [Fact]
    public async Task RefreshAccessTokenAsync_WhenRefreshTokenExprired_ShouldThrownInvalidRefreshTokenException()
    {
      var userRepo = Substitute.For<IUserRepository>();
      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();

      var token_hash = Hash.Sha256(REFRESH_TOKEN);
      var now = DateTime.UtcNow;

      var refresh_token = new RefreshToken
      {
        UserId = USER_ID,
        TokenHash = token_hash,
        CreatedAt = now.AddMinutes(-5),
        ExpiresAt = now.AddMinutes(-2),
      };

      refreshRepo.GetByTokenHashAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns((RefreshToken?)null);
      refreshRepo.UpdateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);
      refreshRepo.CreateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var act = async () => await svc.RefreshAccessTokenAsync(default!, CancellationToken.None);

      await act.Should().ThrowAsync<InvalidRefreshTokenException>();
      await refreshRepo.Received(1).ReceivedWithAnyArgs().GetByTokenHashAsync(default!, default);
      await refreshRepo.DidNotReceive().UpdateOneAsync(default!, default);
      await refreshRepo.DidNotReceive().CreateOneAsync(default!, default);
    }

    [Fact]
    public async Task RefreshAccessTokenAsync_WhenTokenValid_ShouldReturnToken_AndStoreRefreshToken()
    {
      var userRepo = Substitute.For<IUserRepository>();
      var refreshRepo = Substitute.For<IRefreshTokenRepository>();
      var validation = Substitute.For<IValidationRunner>();

      var token_hash = Hash.Sha256(REFRESH_TOKEN);
      var now = DateTime.UtcNow;

      var refresh_token = new RefreshToken
      {
        UserId = USER_ID,
        TokenHash = token_hash,
        CreatedAt = now.AddMinutes(-5),
        ExpiresAt = now.AddDays(1),
      };

      refreshRepo.GetByTokenHashAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
        .Returns(refresh_token);
      refreshRepo.UpdateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);
      refreshRepo.CreateOneAsync(Arg.Any<RefreshToken>(), Arg.Any<CancellationToken>())
        .Returns(Task.CompletedTask);

      var svc = new AuthService(userRepo, refreshRepo, GenerateJwtOptions(), validation);

      var result = await svc.RefreshAccessTokenAsync(token_hash, CancellationToken.None);

      result.Should().NotBeNull();
      result.Result.Data.Should().NotBeNull();
      result.Result.Data.RefreshToken.Should().NotBeNull();
      result.Result.Data.Response.AccessToken.Should().NotBeNull();

      await refreshRepo.Received(1).ReceivedWithAnyArgs().GetByTokenHashAsync(default!, default);
      await refreshRepo.Received(1).ReceivedWithAnyArgs().UpdateOneAsync(default!, default);
      await refreshRepo.Received(1).ReceivedWithAnyArgs().CreateOneAsync(default!, default);
    }
  }

}
