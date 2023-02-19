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
        var webHostBuilder = WebHost.CreateDefaultBuilder();
        webHostBuilder.UseStartup<Startup>();

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
        await Test(Constants.Endpoints.Unauthorized, HttpStatusCode.Unauthorized, Constants.Messages.Unauthorized);
    }
    
    [Test]
    public async Task Forbidden_Test()
    {
        await Test(Constants.Endpoints.Forbidden, HttpStatusCode.Forbidden, Constants.Messages.Forbidden);
    }
    
    [Test]
    public async Task NotFound_Test()
    {
        await Test(Constants.Endpoints.NotFound, HttpStatusCode.NotFound, Constants.Messages.NotFound);
    }

    [Test]
    public async Task TooManyRequests_Test()
    {
        await Test(
            Constants.Endpoints.TooManyRequests,
            HttpStatusCode.TooManyRequests,
            Constants.Messages.TooManyRequests);
    }
    
    [Test]
    public async Task Internal_Test()
    {
        await Test(Constants.Endpoints.Internal, HttpStatusCode.InternalServerError, Constants.Messages.Internal);
    }

    private async Task Test(
        string endpoint,
        HttpStatusCode responseCode,
        string responseMessage)
    {
        var response = await server.CreateClient().GetAsync($"{UrlPrefix}/{endpoint}");
        var responseContent = await response.Content.ReadFromJsonAsync<ErrorResult>();
        if (responseContent is null)
        {
            Assert.Fail(EmptyContentErrorMessage);
            return;            
        }

        Assert.IsTrue(response.StatusCode == responseCode);
        Assert.IsTrue(responseContent.Code == responseCode);

        if (string.IsNullOrEmpty(responseContent.Message))
        {
            Assert.Fail(NoMessageInContent);
            return;
        }
        
        Assert.IsTrue(responseContent.Message == responseMessage);
    }
}