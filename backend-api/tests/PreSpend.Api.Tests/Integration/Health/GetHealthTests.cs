using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PreSpend.Api.Features.Health.GetHealth;
using System.Net;
using System.Net.Http.Json;

namespace PreSpend.Api.Tests.Integration.Health;

public sealed class GetHealthTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GetHealthTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetHealth_ReturnsOk()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<GetHealthResponse>();
        body.Should().NotBeNull();
        body!.Status.Should().Be("ok");
    }
}
