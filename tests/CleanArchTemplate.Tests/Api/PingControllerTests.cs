using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CleanArchTemplate.Tests.Api;

public sealed class PingControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PingControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Should_Return_Pong()
    {
        var response = await _client.GetAsync("/api/ping");
        var body = await response.Content.ReadFromJsonAsync<PingResponseBody>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal("pong", body.Message);
    }

    private sealed record PingResponseBody(string Message, DateTime Timestamp);
}
