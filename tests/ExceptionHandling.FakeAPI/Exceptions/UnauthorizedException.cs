namespace ExceptionHandling.FakeAPI.Exceptions;

public sealed class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}