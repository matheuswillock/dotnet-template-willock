using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CleanArchTemplate.Tests.Api;

public sealed class HealthCheckTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HealthCheckTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_Should_Return_Healthy_Status()
    {
        var response = await _client.GetAsync("/health");
        var body = await response.Content.ReadFromJsonAsync<HealthResponseBody>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(body);
        Assert.Equal("Healthy", body.Status);
    }

    private sealed record HealthResponseBody(string Status);
}
