namespace ExceptionHandling.FakeAPI.Exceptions;

public sealed class TooManyRequestsException : Exception
{
    public TooManyRequestsException(string message) : base(message)
    {
    }
}