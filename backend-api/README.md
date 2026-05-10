# PreSpend API

ASP.NET Core API for BudgeMe, organized with FastEndpoints and vertical feature slices.

## Local Commands

```powershell
dotnet restore PreSpend.slnx
dotnet build PreSpend.slnx --no-restore /p:UseAppHost=false
dotnet test PreSpend.slnx --no-restore
dotnet run --project src/PreSpend.Api/PreSpend.Api.csproj
```

## Local Secrets

Use `backend-api/.env` for local-only secrets. The file is ignored by git.

Use ASP.NET Core environment variable names with double underscores, such as `ConnectionStrings__DefaultConnection` and `Supabase__Url`.
The API loads this file in Development and binds `ConnectionStrings` through typed persistence options.

## Current Baseline

- Minimal API project: `src/PreSpend.Api`
- Test project: `tests/PreSpend.Api.Tests`
- Health endpoint: `GET /api/health`
- Scalar API reference in Development: `/scalar`
- OpenAPI document in Development: `/openapi/v1.json`
