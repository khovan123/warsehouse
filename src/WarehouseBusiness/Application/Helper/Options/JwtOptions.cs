using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Application.Helper.Options;

public sealed class JwtOptions
{
  [Required]
  [ConfigurationKeyName("SECRET_KEY")]
  public string SecretKey { get; init; } = string.Empty;

  [Required]
  [ConfigurationKeyName("ISSUER")]
  public string Issuer { get; init; } = string.Empty;

  [Required]
  [ConfigurationKeyName("AUDIENCE")]
  public string Audience { get; init; } = string.Empty;

  [Range(1, 60 * 24 * 30)]
  [ConfigurationKeyName("TOKEN_EXPIRES")]
  public int TokenExpiresMinutes { get; init; } = 60;
}
