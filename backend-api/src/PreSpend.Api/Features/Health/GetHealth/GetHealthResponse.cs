namespace PreSpend.Api.Features.Health.GetHealth;

public sealed record GetHealthResponse(string Status, DateTimeOffset CheckedAt);
