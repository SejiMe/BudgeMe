---
title: Backend API Project Structure
description: FastEndpoints + ASP.NET Core backend structure using Vertical Slice Architecture and EF Core Code-First.
---

# Backend API Project Structure

This document defines the recommended backend API structure for the pre-spending decision platform. The backend uses **ASP.NET Core**, **FastEndpoints**, **Vertical Slice Architecture**, **EF Core Code-First**, **PostgreSQL via Supabase**, and **Supabase Auth**.

The backend should be organized around product behavior, not database tables. Features should follow the product loop:

```text
User setup -> spending plan planning -> activity execution -> cashflow logging -> line item breakdown -> insights -> prompts
```

---

## Naming Convention

- Use **PascalCase** for C# files, classes, records, enums, and folders that map to namespaces.
  - Example: `CreateActivityEndpoint.cs`, `ApplicationDbContext.cs`, `UserFinancialProfile.cs`
- Use **kebab-case** only for documentation, scripts, or non-C# tooling folders.
  - Example: `.agents/references/backend-api-project-structure.md`
- Use feature-first naming.
  - Good: `CreateSpendingPlanEndpoint.cs`
- Avoid: `SpendingPlanController.cs`
- Use clear endpoint action names.
  - `CreateActivity`
  - `GetActivityDetails`
  - `CompleteActivity`
  - `CreateCashflowEntry`
  - `GenerateActivityInsights`
- Do not create generic controller-style folders such as `Controllers/`, `DTOs/`, or `Repositories/` unless there is a specific reason.

---

## Architecture Rules

### 1. Use Vertical Slice Architecture

Each feature owns its request, response, endpoint, validator, and local behavior. Avoid spreading a single feature across many global folders.

A slice should answer:

> "What user action or system behavior does this implement?"

Examples:

- `Activities/CreateActivity`
- `SpendingPlans/CreateSpendingPlan`
- `Cashflow/CreateCashflowEntry`
- `Insights/GetActivityInsights`
- `Prompts/ScheduleUserPrompt`

---

### 2. Use FastEndpoints, Not Controllers

All HTTP APIs should be implemented using **FastEndpoints**.

Each endpoint should usually contain:

```text
Endpoint.cs
Request.cs
Response.cs
Validator.cs
```

Optional files:

```text
Mapper.cs
Result.cs
Rules.cs
Service.cs
```

Use optional files only when the slice becomes too large.

---

### 3. Use EF Core Code-First

This project should not follow a database-first structure.

Database shape is defined through:

- Entity classes
- EF Core configuration classes
- `ApplicationDbContext`
- EF Core migrations

The ERD is a **reference**, not the source of truth. The source of truth is the code-first entity model plus migrations.

---

### 4. Keep Domain Behavior Close to the Feature

For MVP, keep business logic inside the slice unless it is reused by multiple features.

Move logic into domain services only when duplication appears.

Good MVP approach:

```text
Features/Activities/CompleteActivity/CompleteActivityEndpoint.cs
Features/Activities/CompleteActivity/CompleteActivityRules.cs
```

Avoid premature abstraction.

---

### 5. Use DbContext Directly in Slices

For most MVP features, inject `ApplicationDbContext` directly into the endpoint or slice service.

Avoid generic repositories unless there is a real technical need.

Preferred:

```csharp
public sealed class CreateActivityEndpoint(ApplicationDbContext db) : Endpoint<CreateActivityRequest, CreateActivityResponse>
```

Avoid:

```csharp
IRepository<Activity>
```

---

### 6. Use the ASP.NET Core Options Pattern

Use typed options classes for application settings and integration configuration instead of reading raw configuration values throughout endpoints or services.

Preferred:

```csharp
builder.Services
    .AddOptions<SupabaseJwtOptions>()
    .Bind(builder.Configuration.GetSection("Supabase"))
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

Rules:

- Put cross-cutting option classes under `Common/Settings`.
- Put integration-specific option classes near the integration, such as `Infrastructure/Auth/SupabaseJwtOptions.cs` or `Infrastructure/Email/EmailOptions.cs`.
- Validate required options at startup for infrastructure that cannot work without them.
- Inject `IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>` into infrastructure services instead of passing loose strings.
- Do not read secrets directly inside feature endpoints.
- Local developer secrets may live in ignored `.env` files, but production configuration should come from the hosting provider's secret/configuration system.

---

## Recommended Solution Structure

```ascii
backend-api/
+--- src/
|    +--- PreSpend.Api/
|    |    +--- Program.cs
|    |    +--- appsettings.json
|    |    +--- appsettings.Development.json
|    |    |
|    |    +--- Common/
|    |    |    +--- Constants/
|    |    |    +--- Exceptions/
|    |    |    +--- Extensions/
|    |    |    +--- Middleware/
|    |    |    +--- Results/
|    |    |    +--- Security/
|    |    |    +--- Settings/
|    |    |    +--- Time/
|    |    |
|    |    +--- Features/
|    |    |    +--- Auth/
|    |    |    |    +--- GetCurrentUser/
|    |    |    |    |    +--- GetCurrentUserEndpoint.cs
|    |    |    |    |    +--- GetCurrentUserResponse.cs
|    |    |    |    +--- SyncSupabaseUser/
|    |    |    |         +--- SyncSupabaseUserEndpoint.cs
|    |    |    |         +--- SyncSupabaseUserRequest.cs
|    |    |    |         +--- SyncSupabaseUserResponse.cs
|    |    |    |         +--- SyncSupabaseUserValidator.cs
|    |    |    |
|    |    |    +--- Users/
|    |    |    |    +--- CompleteOnboarding/
|    |    |    |    +--- GetUserProfile/
|    |    |    |    +--- UpdateUserProfile/
|    |    |    |
|    |    |    +--- FinancialProfiles/
|    |    |    |    +--- GetFinancialProfile/
|    |    |    |    +--- UpdateFinancialProfile/
|    |    |    |
|    |    |    +--- Categories/
|    |    |    |    +--- CreateCategory/
|    |    |    |    +--- GetCategories/
|    |    |    |    +--- SeedSystemCategories/
|    |    |    |    +--- UpdateCategory/
|    |    |    |
|    |    |    +--- Activities/
|    |    |    |    +--- CreateActivity/
|    |    |    |    +--- GetActivityDetails/
|    |    |    |    +--- GetActivities/
|    |    |    |    +--- CompleteActivity/
|    |    |    |    +--- CancelActivity/
|    |    |    |
|    |    |    +--- SpendingPlans/
|    |    |    |    +--- CreateSpendingPlan/
|    |    |    |    +--- GetSpendingPlan/
|    |    |    |    +--- UpdateSpendingPlan/
|    |    |    |    +--- AddSpendingPlanItem/
|    |    |    |    +--- UpdateSpendingPlanItem/
|    |    |    |    +--- RemoveSpendingPlanItem/
|    |    |    |
|    |    |    +--- SpendingPlanTemplates/
|    |    |    |    +--- CreateSpendingPlanTemplate/
|    |    |    |    +--- GetSpendingPlanTemplates/
|    |    |    |    +--- CreateSpendingPlanFromTemplate/
|    |    |    |    +--- UpdateSpendingPlanTemplate/
|    |    |    |
|    |    |    +--- Cashflow/
|    |    |    |    +--- CreateCashflowEntry/
|    |    |    |    +--- GetCashflowEntries/
|    |    |    |    +--- GetCashflowSummary/
|    |    |    |    +--- UpdateCashflowEntry/
|    |    |    |    +--- DeleteCashflowEntry/
|    |    |    |
|    |    |    +--- LineItems/
|    |    |    |    +--- AddLineItem/
|    |    |    |    +--- UpdateLineItem/
|    |    |    |    +--- RemoveLineItem/
|    |    |    |    +--- MatchLineItemToSpendingPlanItem/
|    |    |    |
|    |    |    +--- Insights/
|    |    |    |    +--- GenerateActivityInsights/
|    |    |    |    +--- GetUserInsights/
|    |    |    |    +--- MarkInsightSeen/
|    |    |    |    +--- Rules/
|    |    |    |
|    |    |    +--- Prompts/
|    |    |    |    +--- GetUserPrompts/
|    |    |    |    +--- ScheduleUserPrompt/
|    |    |    |    +--- MarkPromptActed/
|    |    |    |    +--- DismissPrompt/
|    |    |    |
|    |    |    +--- BehaviorMetrics/
|    |    |         +--- GenerateBehaviorMetrics/
|    |    |         +--- GetBehaviorMetrics/
|    |    |
|    |    +--- Domain/
|    |    |    +--- Entities/
|    |    |    |    +--- User.cs
|    |    |    |    +--- UserFinancialProfile.cs
|    |    |    |    +--- UserAuthIdentity.cs
|    |    |    |    +--- Category.cs
|    |    |    |    +--- Activity.cs
|    |    |    |    +--- CashflowEntry.cs
|    |    |    |    +--- LineItem.cs
|    |    |    |    +--- SpendingPlan.cs
|    |    |    |    +--- SpendingPlanItem.cs
|    |    |    |    +--- SpendingPlanTemplate.cs
|    |    |    |    +--- SpendingPlanTemplateItem.cs
|    |    |    |    +--- InsightRule.cs
|    |    |    |    +--- UserInsight.cs
|    |    |    |    +--- PromptTemplate.cs
|    |    |    |    +--- UserPrompt.cs
|    |    |    |    +--- UserBehaviorMetric.cs
|    |    |    +--- Enums/
|    |    |    +--- ValueObjects/
|    |    |    +--- DomainErrors.cs
|    |    |
|    |    +--- Infrastructure/
|    |         +--- Persistence/
|    |         |    +--- ApplicationDbContext.cs
|    |         |    +--- DesignTimeDbContextFactory.cs
|    |         |    +--- Configurations/
|    |         |    |    +--- UserConfiguration.cs
|    |         |    |    +--- UserFinancialProfileConfiguration.cs
|    |         |    |    +--- CategoryConfiguration.cs
|    |         |    |    +--- ActivityConfiguration.cs
|    |         |    |    +--- CashflowEntryConfiguration.cs
|    |         |    |    +--- LineItemConfiguration.cs
|    |         |    |    +--- SpendingPlanConfiguration.cs
|    |         |    |    +--- SpendingPlanItemConfiguration.cs
|    |         |    |    +--- SpendingPlanTemplateConfiguration.cs
|    |         |    |    +--- InsightRuleConfiguration.cs
|    |         |    |    +--- PromptTemplateConfiguration.cs
|    |         |    |    +--- UserPromptConfiguration.cs
|    |         |    |    +--- UserBehaviorMetricConfiguration.cs
|    |         |    +--- Migrations/
|    |         |    +--- Seeders/
|    |         |         +--- SystemCategorySeeder.cs
|    |         |         +--- InsightRuleSeeder.cs
|    |         |         +--- PromptTemplateSeeder.cs
|    |         |
|    |         +--- Auth/
|    |         |    +--- SupabaseJwtOptions.cs
|    |         |    +--- SupabaseJwtValidator.cs
|    |         |    +--- CurrentUserService.cs
|    |         |
|    |         +--- Email/
|    |         |    +--- ResendEmailService.cs
|    |         |    +--- EmailOptions.cs
|    |         |
|    |         +--- Observability/
|    |         |    +--- SerilogExtensions.cs
|    |         |    +--- OpenTelemetryExtensions.cs
|    |         |
|    |         +--- Jobs/
|    |         |    +--- QuartzExtensions.cs
|    |         |    +--- PromptSchedulingJob.cs
|    |         |
|    |         +--- Caching/
|    |         |    +--- RedisExtensions.cs
|    |         |
|    |         +--- Integrations/
|    |              +--- Supabase/
|    |              +--- Resend/
|    |
+--- tests/
|    +--- PreSpend.Api.Tests/
|         +--- Integration/
|         |    +--- Activities/
|         |    +--- SpendingPlans/
|         |    +--- Cashflow/
|         |    +--- Insights/
|         +--- Unit/
|         |    +--- Domain/
|         |    +--- Rules/
|         +--- Fixtures/
|         |    +--- ApiFactory.cs
|         |    +--- PostgresContainerFixture.cs
|         +--- TestData/
|
+--- .agents/
|    +--- references/
|         +--- backend-api-project-structure.md
|         +--- erd.md
|         +--- api-conventions.md
|
+--- docker/
|    +--- Dockerfile
|    +--- docker-compose.yml
|
+--- .github/
|    +--- workflows/
|         +--- backend-ci.yml
|
+--- PreSpend.sln
+--- README.md
+--- AGENTS.md
```

---

## Feature Slice Template

Each FastEndpoints slice should use this structure:

```ascii
Features/
+--- Activities/
|    +--- CreateActivity/
|         +--- CreateActivityEndpoint.cs
|         +--- CreateActivityRequest.cs
|         +--- CreateActivityResponse.cs
|         +--- CreateActivityValidator.cs
|         +--- CreateActivityMapper.cs              # optional
|         +--- CreateActivityRules.cs               # optional
```

### Endpoint Rule

Each endpoint should be small enough to read quickly. It should handle:

1. Route configuration
2. Authentication / authorization requirements
3. Request validation through validator
4. Calling local slice logic or `ApplicationDbContext`
5. Returning response DTO

Avoid placing unrelated feature logic inside the endpoint.

---

## Code-First Persistence Structure

Use EF Core configurations instead of bloating `OnModelCreating`.

```ascii
Infrastructure/
+--- Persistence/
|    +--- ApplicationDbContext.cs
|    +--- DesignTimeDbContextFactory.cs
|    +--- Configurations/
|    |    +--- ActivityConfiguration.cs
|    |    +--- SpendingPlanConfiguration.cs
|    |    +--- CashflowEntryConfiguration.cs
|    |    +--- LineItemConfiguration.cs
|    +--- Migrations/
|    +--- Seeders/
```

### Entity Rules

- Entity names should be singular.
  - `Activity`, not `Activities`
  - `SpendingPlan`, not `SpendingPlans`
- Database table names may remain plural through configuration.
- Use `Guid` for primary keys.
- Use `decimal` for money values.
- Use UTC timestamps for `CreatedAt`, `UpdatedAt`, `SeenAt`, etc.
- Use enums in code for controlled values where possible.
  - `ActivityStatus`
  - `EntryType`
  - `Classification`
  - `PromptStatus`
  - `InsightSeverity`

---

## Suggested Domain Entity Mapping

The current ERD maps well into these entity files:

```ascii
Domain/Entities/
+--- User.cs
+--- UserFinancialProfile.cs
+--- UserAuthIdentity.cs
+--- Category.cs
+--- Activity.cs
+--- CashflowEntry.cs
+--- LineItem.cs
+--- SpendingPlan.cs
+--- SpendingPlanItem.cs
+--- SpendingPlanTemplate.cs
+--- SpendingPlanTemplateItem.cs
+--- InsightRule.cs
+--- UserInsight.cs
+--- PromptTemplate.cs
+--- UserPrompt.cs
+--- UserBehaviorMetric.cs
```

Do not create entity classes based on frontend screens. Entities should follow durable business concepts.

---

## MVP Feature Groups

### Auth

Handles Supabase user synchronization and current-user resolution.

Suggested endpoints:

```text
GET    /api/me
POST   /api/auth/sync
```

---

### Users

Handles user profile and onboarding state.

Suggested endpoints:

```text
GET    /api/users/me
PATCH  /api/users/me
POST   /api/users/complete-onboarding
```

---

### Financial Profiles

Handles income frequency, payday expectations, and timezone.

Suggested endpoints:

```text
GET    /api/financial-profile
PUT    /api/financial-profile
```

---

### Activities

Central behavior object for shopping, grocery, bills, going out, and other spending moments.

Suggested endpoints:

```text
POST   /api/activities
GET    /api/activities
GET    /api/activities/{id}
PATCH  /api/activities/{id}
POST   /api/activities/{id}/complete
POST   /api/activities/{id}/cancel
```

---

### SpendingPlans

Handles pre-spending to-buy lists.

Suggested endpoints:

```text
POST   /api/activities/{activityId}/spending-plan
GET    /api/spending-plans/{id}
PATCH  /api/spending-plans/{id}
POST   /api/spending-plans/{id}/items
PATCH  /api/spending-plan-items/{id}
DELETE /api/spending-plan-items/{id}
```

---

### Spending Plan Templates

Handles reusable grocery or shopping plans.

Suggested endpoints:

```text
POST   /api/spending-plan-templates
GET    /api/spending-plan-templates
POST   /api/spending-plan-templates/{id}/create-spending-plan
PATCH  /api/spending-plan-templates/{id}
DELETE /api/spending-plan-templates/{id}
```

---

### Cashflow

Handles money in and money out.

Suggested endpoints:

```text
POST   /api/cashflow-entries
GET    /api/cashflow-entries
GET    /api/cashflow-summary
PATCH  /api/cashflow-entries/{id}
DELETE /api/cashflow-entries/{id}
```

---

### Line Items

Handles itemized spending details.

Suggested endpoints:

```text
POST   /api/activities/{activityId}/line-items
PATCH  /api/line-items/{id}
DELETE /api/line-items/{id}
POST   /api/line-items/{id}/match-spending-plan-item
```

---

### Insights

Handles rule-based planned vs actual feedback.

Suggested endpoints:

```text
POST   /api/activities/{activityId}/insights/generate
GET    /api/insights
POST   /api/insights/{id}/seen
```

---

### Prompts

Handles user nudges and prompt lifecycle.

Suggested endpoints:

```text
GET    /api/prompts
POST   /api/prompts/{id}/acted
POST   /api/prompts/{id}/dismissed
```

---

## Infrastructure Guidance

### Supabase Auth

The backend should trust Supabase JWTs after validation. Application users should still be stored in the local `Users` table so the domain model can reference `UserId` consistently.

### Supabase PostgreSQL

Use Supabase as the hosted PostgreSQL provider. EF Core migrations should define schema changes.

### Resend

Use Resend for transactional emails only. Do not mix email concerns into feature slices.

### Quartz

Quartz is a later-phase concern for reminders and scheduled prompt generation. Keep it in `Infrastructure/Jobs`.

### Redis

Redis is later-phase caching. Do not introduce caching until there is a measured performance need.

### Serilog and OpenTelemetry

All requests should be logged with structured context. Do not log sensitive user data, access tokens, or full financial notes.

---

## Testing Structure

Use xUnit, FluentAssertions, and Testcontainers.

```ascii
tests/
+--- PreSpend.Api.Tests/
|    +--- Integration/
|    |    +--- Activities/
|    |    +--- SpendingPlans/
|    |    +--- Cashflow/
|    |    +--- Insights/
|    +--- Unit/
|    |    +--- Rules/
|    |    +--- Domain/
|    +--- Fixtures/
|    |    +--- ApiFactory.cs
|    |    +--- PostgresContainerFixture.cs
|    +--- TestData/
```

### Test Priorities

Start with tests for the core loop:

```text
Create spending plan -> Add spending plan items -> Create spending activity -> Add line items -> Generate insights
```

Also test:

- Income entry creation
- Random expense creation
- Planned vs actual variance
- Need vs want classification
- Prompt lifecycle actions

---

## Migration Rules

- Create migrations from code changes only.
- Do not manually modify generated migration files unless necessary.
- Use clear migration names:
  - `InitialCreate`
  - `AddSpendingPlanTemplates`
  - `AddPromptLifecycleFields`
- Seed only stable system data:
  - System categories
  - Insight rules
  - Prompt templates

---

## Agent Rules for AI Coding Assistants

AI coding agents must follow these rules:

1. Do not create controllers. Use FastEndpoints.
2. Do not implement database-first scaffolding.
3. Do not create generic repositories unless explicitly requested.
4. Do not add AI/LLM features in the MVP unless specifically assigned.
5. Do not add inventory features.
6. Keep features vertical and self-contained.
7. Prefer simple rule-based insights.
8. Keep endpoint request/response models feature-local.
9. Respect the ERD reference, but implement schema through EF Core Code-First.
10. When adding a feature, add or update tests for the related slice.

---

## Project Structure Decision

Recommended starting mode:

```text
Single API project + test project
```

Use this first:

```text
PreSpend.Api
PreSpend.Api.Tests
```

Split into multiple projects only when the codebase demands it:

```text
PreSpend.Api
PreSpend.Domain
PreSpend.Infrastructure
PreSpend.Application
PreSpend.Api.Tests
```

For MVP, avoid over-splitting. A clean single API project with strong folders is easier for a small team and AI coding agents to navigate.

---

## Final Principle

Build around the loop, not the tables:

```text
Plan before spending -> record what happened -> show what changed -> nudge the next cycle
```

That is the backend's mission.
