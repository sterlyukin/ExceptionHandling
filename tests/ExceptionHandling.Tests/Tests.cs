using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ExceptionHandling.FakeAPI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace ExceptionHandling.Tests;

public class Tests
{
    private const string UrlPrefix = "/api/fake";

    private const string EmptyContentErrorMessage = "Response content is empty";
    private const string NoMessageInContent = "Response content doesn't contain message";
    
    private readonly TestServer server;

    public Tests()
    {
        var webHostBuilder = WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>();
        
        /*var webHostBuilder = WebHost.CreateDefaultBuilder();
        
        WebHostBuilderExtensions.UseStartup<Startup>(webHostBuilder);
        webHostBuilder.UseStartup<Startup>();*/

        server = new TestServer(webHostBuilder);
    }

    [Test]
    public async Task Success_Test()
    {
        var response = await server.CreateClient().GetAsync($"{UrlPrefix}/{Constants.Endpoints.Success}");
        
        Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
    }
    
    [Test]
    public async Task Unauthorized_Test()
    {
        var response = await server.CreateClient().GetAsync($"{UrlPrefix}/{Constants.Endpoints.Unauthorized}");
        var responseContent = await response.Content.ReadFromJsonAsync<ErrorResult>();
        if (responseContent is null)
        {
            Assert.Fail(EmptyContentErrorMessage);
            return;            
        }

        Assert.IsTrue(response.StatusCode == HttpStatusCode.Unauthorized);
        Assert.IsTrue(responseContent.Code == HttpStatusCode.Unauthorized);

        if (string.IsNullOrEmpty(responseContent.Message))
        {
            Assert.Fail(NoMessageInContent);
            return;
        }
        
        Assert.IsTrue(responseContent.Message == Constants.Messages.Unauthorized);
    }
    
    [Test]
    public async Task Forbidden_Test()
    {
        var response = await server.CreateClient().GetAsync($"{UrlPrefix}/{Constants.Endpoints.Forbidden}");
        var responseContent = await response.Content.ReadFromJsonAsync<ErrorResult>();
        if (responseContent is null)
        {
            Assert.Fail(EmptyContentErrorMessage);
            return;            
        }

        Assert.IsTrue(response.StatusCode == HttpStatusCode.Forbidden);
        Assert.IsTrue(responseContent.Code == HttpStatusCode.Forbidden);

        if (string.IsNullOrEmpty(responseContent.Message))
        {
            Assert.Fail(NoMessageInContent);
            return;
        }
        
        Assert.IsTrue(responseContent.Message == Constants.Messages.Forbidden);
    }
    
    [Test]
    public async Task NotFound_Test()
    {
        var response = await server.CreateClient().GetAsync($"{UrlPrefix}/{Constants.Endpoints.NotFound}");
        var responseContent = await response.Content.ReadFromJsonAsync<ErrorResult>();
        if (responseContent is null)
        {
            Assert.Fail(EmptyContentErrorMessage);
            return;            
        }

        Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        Assert.IsTrue(responseContent.Code == HttpStatusCode.NotFound);

        if (string.IsNullOrEmpty(responseContent.Message))
        {
            Assert.Fail(NoMessageInContent);
            return;
        }
        
        Assert.IsTrue(responseContent.Message == Constants.Messages.NotFound);
    }
    
    [Test]
    public async Task Internal_Test()
    {
        var response = await server.CreateClient().GetAsync($"{UrlPrefix}/{Constants.Endpoints.Internal}");
        var responseContent = await response.Content.ReadFromJsonAsync<ErrorResult>();
        if (responseContent is null)
        {
            Assert.Fail(EmptyContentErrorMessage);
            return;            
        }

        Assert.IsTrue(response.StatusCode == HttpStatusCode.InternalServerError);
        Assert.IsTrue(responseContent.Code == HttpStatusCode.InternalServerError);

        if (string.IsNullOrEmpty(responseContent.Message))
        {
            Assert.Fail(NoMessageInContent);
            return;
        }
        
        Assert.IsTrue(responseContent.Message == Constants.Messages.Internal);
    }
}