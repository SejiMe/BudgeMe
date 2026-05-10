# BudgeMe

BudgeMe is a pre-spending decision platform for planning purchases before spending, recording actual activity after spending, comparing plan against reality, and reflecting through simple insights.

## Project Structure

- `backend-api/` - ASP.NET Core + FastEndpoints API.
- `mobile-app/` - Expo mobile app. This will be scaffolded after the backend foundation is stable.

## Backend

```powershell
dotnet build backend-api/PreSpend.slnx --no-restore /p:UseAppHost=false
dotnet test backend-api/PreSpend.slnx --no-restore
```

## Mobile App

```powershell
cd mobile-app
npm.cmd run typecheck
npm.cmd run start
```
