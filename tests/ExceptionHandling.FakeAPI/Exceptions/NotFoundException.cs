namespace ExceptionHandling.FakeAPI.Exceptions;

public sealed class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message)
    {
    }
}