using System.Net;
using ExceptionHandling.FakeAPI.Exceptions;
using ExceptionHandling.FakeAPI.Services;

namespace ExceptionHandling.FakeAPI;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<FakeService>();
        services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.AddExceptionHandling<UnauthorizedException>(HttpStatusCode.Unauthorized);
        app.AddExceptionHandling<ForbiddenException>(HttpStatusCode.Forbidden);
        app.AddExceptionHandling<NotFoundException>(HttpStatusCode.NotFound);
        app.AddExceptionHandling<TooManyRequestsException>(HttpStatusCode.TooManyRequests);

        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}