using ExceptionHandling.FakeAPI.Exceptions;

namespace ExceptionHandling.FakeAPI.Services;

public sealed class FakeService
{
    public void GetSuccess()
    {
    }

    public void GetUnauthorized()
    {
        throw new UnauthorizedException(Constants.Messages.Unauthorized);
    }

    public void GetForbiddenAccess()
    {
        throw new ForbiddenException(Constants.Messages.Forbidden);
    }

    public void GetNotFound()
    {
        throw new NotFoundException(Constants.Messages.NotFound);
    }

    public void GetTooManyRequests()
    {
        throw new TooManyRequestsException(Constants.Messages.TooManyRequests);
    }

    public void GetInternal()
    {
        throw new Exception(Constants.Messages.Internal);
    }
}