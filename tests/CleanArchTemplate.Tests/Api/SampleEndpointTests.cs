using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CleanArchTemplate.Tests.Api;

public sealed class SampleEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SampleEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateSample_Should_Return_Created_Without_PostgreSql_In_Development()
    {
        var response = await _client.PostAsJsonAsync("/api/samples", new { name = "Sample" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}
