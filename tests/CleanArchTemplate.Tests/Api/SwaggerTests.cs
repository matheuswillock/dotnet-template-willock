using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CleanArchTemplate.Tests.Api;

public sealed class SwaggerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SwaggerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SwaggerJson_Should_Be_Available()
    {
        var response = await _client.GetAsync("/swagger/v1/swagger.json");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
