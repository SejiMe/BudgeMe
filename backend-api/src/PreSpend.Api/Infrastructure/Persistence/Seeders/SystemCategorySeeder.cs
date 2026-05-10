using Microsoft.EntityFrameworkCore;
using PreSpend.Api.Domain.Entities;
using PreSpend.Api.Domain.Enums;

namespace PreSpend.Api.Infrastructure.Persistence.Seeders;

public static class SystemCategorySeeder
{
    public const int ExpectedCount = 11;

    public static readonly Guid GroceriesId = Guid.Parse("11111111-1111-1111-1111-111111111001");
    public static readonly Guid BillsId = Guid.Parse("11111111-1111-1111-1111-111111111002");
    public static readonly Guid TransportId = Guid.Parse("11111111-1111-1111-1111-111111111003");
    public static readonly Guid FoodDrinkId = Guid.Parse("11111111-1111-1111-1111-111111111004");
    public static readonly Guid HouseholdId = Guid.Parse("11111111-1111-1111-1111-111111111005");
    public static readonly Guid HealthId = Guid.Parse("11111111-1111-1111-1111-111111111006");
    public static readonly Guid PersonalCareId = Guid.Parse("11111111-1111-1111-1111-111111111007");
    public static readonly Guid ShoppingId = Guid.Parse("11111111-1111-1111-1111-111111111008");
    public static readonly Guid SocialId = Guid.Parse("11111111-1111-1111-1111-111111111009");
    public static readonly Guid IncomeId = Guid.Parse("11111111-1111-1111-1111-111111111010");
    public static readonly Guid OtherId = Guid.Parse("11111111-1111-1111-1111-111111111011");

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            Create(GroceriesId, "groceries", CategoryType.Expense),
            Create(BillsId, "bills", CategoryType.Expense),
            Create(TransportId, "transport", CategoryType.Expense),
            Create(FoodDrinkId, "food-drink", CategoryType.Expense),
            Create(HouseholdId, "household", CategoryType.Expense),
            Create(HealthId, "health", CategoryType.Expense),
            Create(PersonalCareId, "personal-care", CategoryType.Expense),
            Create(ShoppingId, "shopping", CategoryType.Expense),
            Create(SocialId, "social", CategoryType.Expense),
            Create(IncomeId, "income", CategoryType.Income),
            Create(OtherId, "other", CategoryType.Mixed));
    }

    private static Category Create(Guid id, string name, CategoryType categoryType)
    {
        return new Category
        {
            Id = id,
            UserId = null,
            Name = name,
            CategoryType = categoryType,
            IsSystemDefault = true,
            CreatedAt = StableSeedDataExtensions.SeededAt,
            UpdatedAt = StableSeedDataExtensions.SeededAt
        };
    }
}
