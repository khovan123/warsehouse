namespace Application.Exceptions;

public abstract class DomainException : Exception
{
  protected DomainException(string message = "Domain error") : base(message) { }
}

public sealed class NotFoundException : DomainException
{
  public NotFoundException(string message = "Not found") : base(message) { }
}

public sealed class ConflictException : DomainException
{
  public ConflictException(string message = "Conflict error") : base(message) { }
}

public sealed class ForbiddenException : DomainException
{
  public ForbiddenException(string message = "Forbidden") : base(message) { }
}

public sealed class UnauthorizedException : DomainException
{
  public UnauthorizedException(string message = "Unauthorized") : base(message) { }
}

public sealed class TokenExpiredException : DomainException
{
  public TokenExpiredException(string message = "Token has expired") : base(message) { }
}

public sealed class InvalidRefreshTokenException : DomainException
{
  public InvalidRefreshTokenException(string message = "Refresh token is invalid or expired") : base(message) { }
}
