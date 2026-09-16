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
