namespace ExceptionHandling.FakeAPI.Exceptions;

public sealed class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}