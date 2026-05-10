using FastEndpoints;

namespace PreSpend.Api.Features.Health.GetHealth;

public sealed class GetHealthEndpoint : EndpointWithoutRequest<GetHealthResponse>
{
    public override void Configure()
    {
        Get("/api/health");
        AllowAnonymous();
        Description(builder => builder.WithName("GetHealth"));
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        return Send.OkAsync(new GetHealthResponse("ok", DateTimeOffset.UtcNow), ct);
    }
}
