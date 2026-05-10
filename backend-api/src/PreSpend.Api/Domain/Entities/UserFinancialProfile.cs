using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Domain.Entities;

public sealed class UserFinancialProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public IncomeFrequency IncomeFrequency { get; set; }
    public DateOnly? NextExpectedIncomeDate { get; set; }
    public int? PaydayDayOfMonth { get; set; }
    public string Timezone { get; set; } = "UTC";
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
