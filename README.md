# ExceptionHandling

[![build](https://github.com/sterlyukin/ExceptionHandling/actions/workflows/build.yml/badge.svg)](https://github.com/sterlyukin/ExceptionHandling/actions/workflows/build.yml)
[![test](https://github.com/sterlyukin/ExceptionHandling/actions/workflows/test.yml/badge.svg)](https://github.com/sterlyukin/ExceptionHandling/actions/workflows/test.yml)
[![latest version](https://img.shields.io/nuget/v/Sterlyukin.ExceptionHandling)](https://www.nuget.org/packages/Sterlyukin.ExceptionHandling)
[![downloads](https://img.shields.io/nuget/dt/Sterlyukin.ExceptionHandling)](https://www.nuget.org/packages/Sterlyukin.ExceptionHandling)

This is repository for `ExceptionHandling` opensource library.

`ExceptionHandling` helps to convert thrown exceptions to valid json object.

This object contains:
- HTTP code;
- Error message.

## Installation

```
dotnet add package Sterlyukin.ExceptionHandling
```

## Usage

Create your custom exceptions that are derived from `ApplicationException`

```csharp
public sealed class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message) : base(message)
    {
    }
}

public sealed class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
```

Register each exception with matching HTTP status code

```csharp
app.AddExceptionHandling<UnauthorizedException>(HttpStatusCode.Unauthorized);
app.AddExceptionHandling<NotFoundException>(HttpStatusCode.NotFound);
```

Throw exceptions in your code and they will be translated to response with matching HTTP status code.

You shouldn't to tune library more. After this it is ready for using.

```csharp
public sealed class Service
{
    public void GetSuccess()
    {
    }

    public void GetUnauthorized()
    {
        throw new UnauthorizedException("You are unauthorized");
    }

    public void GetNotFound()
    {
        throw new NotFoundException("Data wasn't found");
    }
}
```

```csharp
[ApiController]
[Route("api/[controller]")]
public class FakeController : ControllerBase
{
    private readonly Service service;

    public FakeController(Service service)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        service.GetUnauthorized();
        return Ok();
    }
    
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        service.GetNotFound();
        return Ok();
    }
}
```

Response from API endpoint will be in this format

```json
{
   "Code": 401,
   "Message": "You are unauthorized"
}
```

## Contribution

Repository is opened for your contribution.

Improve it by creating `Issues` and `Pull requests`.

### Creating Pull request

Pay attention that pull requests can be created only from issues.

Algorithm:

1) Create an issue - describe what do you want to impore/fix in the library;
2) Create new branch from `main` by pattern `issues_{issue number}`;
3) Create pull request to `main` branch;
4) Make sure that all checks passed successfully;
5) Assign `sterlyukin` as reviewer of your pull-request;
6) Link issue to your PR;
7) Wait for comments or approve;
8) If your PR has some comments to fix - fix them and push to the branch again;
9) Leave comments that you fixed under all remarks;
10) When PR will be successfully closed - I will publish it to `nuget`.

## CI/CD

CI/CD automated by Github Actions.
`Build` and `test` are launched  on push to the branch.

`Publish` to nuget launches manually in releases.