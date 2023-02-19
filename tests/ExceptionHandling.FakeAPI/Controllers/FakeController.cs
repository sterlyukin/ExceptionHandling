using ExceptionHandling.FakeAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandling.FakeAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FakeController : ControllerBase
{
    private readonly FakeService service;

    public FakeController(FakeService service)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
    }
    
    [HttpGet(Constants.Endpoints.Success)]
    public IActionResult GetSuccess()
    {
        service.GetSuccess();
        return Ok();
    }
    
    [HttpGet(Constants.Endpoints.Unauthorized)]
    public IActionResult GetUnauthorized()
    {
        service.GetUnauthorized();
        return Ok();
    }
    
    [HttpGet(Constants.Endpoints.Forbidden)]
    public IActionResult GetForbiddenAccess()
    {
        service.GetForbiddenAccess();
        return Ok();
    }

    [HttpGet(Constants.Endpoints.NotFound)]
    public IActionResult GetNotFound()
    {
        service.GetNotFound();
        return Ok();
    }
    
    [HttpGet(Constants.Endpoints.TooManyRequests)]
    public IActionResult GetTooManyRequests()
    {
        service.GetTooManyRequests();
        return Ok();
    }
    
    [HttpGet(Constants.Endpoints.Internal)]
    public IActionResult GetInternal()
    {
        service.GetInternal();
        return Ok();
    }
}