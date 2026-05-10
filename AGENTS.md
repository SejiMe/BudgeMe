# AGENTS.md

## Purpose

This is the primary instruction document for AI coding agents working on BudgeMe. Read this file before making changes, then read the required reference files named below.

BudgeMe is a pre-spending decision platform. It helps users reduce impulse buying by planning purchases before spending, recording actual activity after spending, comparing the plan with reality, and reflecting through simple insights.

The product is not a generic budget tracker. It is a behavioral spending assistant focused on this loop:

```text
Plan Before Spending -> Record Actual Spending -> Compare -> Reflect -> Improve Next Cycle
```

## Non-Negotiable Reference Context

These files are part of the operating instructions for this repository. Treat them as source-of-truth context when planning or editing code:

- `.agents/references/ERD.mmd`
- `.agents/references/backend-api-project-structure.md`
- `.agents/references/frontend_project_structure.md`
- `.ai-workspace/*`

Before making backend, frontend, or data-model changes:

1. Read the relevant reference file.
2. Match its naming, boundaries, and feature structure.
3. Do not replace it with older suggestions from memory.
4. If this file and a reference file conflict, the reference file wins for its area.

The ERD is required domain context. It defines the durable MVP concepts and relationships agents must preserve:

- `Users`
- `UserFinancialProfiles`
- `UserAuthIdentities`
- `Categories`
- `Activities`
- `CashflowEntries`
- `LineItems`
- `SpendingPlans`
- `SpendingPlanItems`
- `SpendingPlanTemplates`
- `SpendingPlanTemplateItems`
- `InsightRules`
- `UserInsights`
- `PromptTemplates`
- `UserPrompts`
- `UserBehaviorMetrics`

The ERD is a reference for the product model. Backend persistence remains EF Core Code-First: entities, configurations, `ApplicationDbContext`, and migrations define the implemented schema.

## AI Workspace Requirement

Agents must always use `.ai-workspace/` for AI working artifacts related to this repository.

Use `.ai-workspace/` for:

- Scratch notes and investigation notes
- Implementation plans
- Plan approval records
- Task boards and task breakdowns
- Worktree coordination notes
- AI agent handoffs
- Parallel agent task context
- Research summaries
- Temporary reports related to coding work

Do not place AI scratchpads, planning files, approval notes, or agent coordination artifacts in the product source tree unless the user explicitly asks for a committed documentation artifact.

If `.ai-workspace/` does not exist and the task needs scratch, planning, approval, worktree, or agent coordination files, create it using the repository's AI workspace conventions before adding those artifacts.

## Product Mission

Help low-to-mid income users, especially young professionals or single-income households, increase financial control by reducing impulse purchases during common spending moments such as payday, groceries, errands, bills, shopping, and planned activities.

## MVP Scope

Build only what strengthens the core loop.

### In Scope

- Authentication with Google through Supabase Auth
- Local application user profile synced from Supabase
- Basic onboarding preferences
- Financial profile setup, including income frequency, payday expectations, and timezone
- Income and expense logging as cashflow entries
- Transaction/cashflow history with filtering
- Spending activities grouped by real-world context
- Spending plans created before spending activities
- Spending plan templates for repeated activities such as groceries
- Planned vs actual comparison
- Item-level line item breakdowns
- Matching actual line items to planned spending plan items
- Rule-based insights
- Helpful prompt/trigger foundations
- Simple behavior metrics tied to the core loop

### Out of Scope for MVP

- Inventory tracking
- Family/shared accounts
- Bank integrations
- AI-generated insights requiring historical data
- SMS reminders
- Redis-dependent caching
- Complex automation workflows
- Social features

Do not add out-of-scope features unless explicitly requested.

## Technology Stack

### Backend

- .NET 10
- ASP.NET Core
- FastEndpoints
- Vertical Slice Architecture
- EF Core Code-First
- PostgreSQL via Supabase
- Supabase Auth with Google Auth
- xUnit, FluentAssertions, and Testcontainers
- Resend for email-related flows only
- Docker and GitHub Actions
- Serilog and OpenTelemetry
- Quartz later
- Redis later
- SMS providers later

### Frontend

- Expo
- Expo Router
- Tamagui
- TanStack Query
- Zustand
- FlashList
- React Hook Form
- Zod
- React Native Reanimated
- React Native Gesture Handler
- @gorhom/bottom-sheet
- @supabase/supabase-js
- expo-auth-session
- expo-linking
- expo-secure-store
- react-native-mmkv
- expo-notifications
- expo-haptics
- lucide-react-native
- date-fns
- React Native Skia
- React Native Testing Library
- Maestro
- ESLint and Prettier

## Architecture Rules

### Use Vertical Slice Architecture

Backend and frontend must be organized around product behavior, not technical layers or database tables first.

A slice should contain the files needed to complete one user action or system behavior. Avoid scattering related logic across unrelated global folders.

Good slice examples:

- `Features/Activities/CreateActivity`
- `Features/SpendingPlans/CreateSpendingPlan`
- `Features/Cashflow/CreateCashflowEntry`
- `src/features/spending-plans`
- `src/features/activities`
- `src/features/cashflow`

Poor structure examples:

- One massive `services/` folder
- One massive `components/` folder
- One global `types.ts` file for everything
- Business logic hidden inside route files or UI components
- Generic repositories without a clear technical need
- Controller-style API folders

## Backend Agent Rules

Follow `.agents/references/backend-api-project-structure.md` for backend structure. That file is the current backend source of truth.

### Backend Project Shape

The default backend mode is:

```text
PreSpend.Api
PreSpend.Api.Tests
```

Use a clean single API project first. Split into `Domain`, `Infrastructure`, or `Application` projects only when the codebase clearly demands it.

### Backend Feature Groups

Use these MVP feature groups unless the user explicitly asks for something else:

- `Auth`
- `Users`
- `FinancialProfiles`
- `Categories`
- `Activities`
- `SpendingPlans`
- `SpendingPlanTemplates`
- `Cashflow`
- `LineItems`
- `Insights`
- `Prompts`
- `BehaviorMetrics`

### FastEndpoints Standards

Use FastEndpoints, not controllers.

Each endpoint slice should usually include:

```text
Endpoint.cs
Request.cs
Response.cs
Validator.cs
```

Optional files are allowed when useful:

```text
Mapper.cs
Result.cs
Rules.cs
Service.cs
```

Keep request and response models feature-local. Keep endpoints readable. If a slice grows, extract local rules or services inside that slice before creating broad abstractions.

### Backend Persistence Rules

- Use EF Core Code-First.
- Use `ApplicationDbContext`.
- Use configuration classes under `Infrastructure/Persistence/Configurations`.
- Use migrations generated from code changes.
- Use `Guid` primary keys.
- Use `decimal` for money.
- Use UTC timestamps for created, updated, seen, scheduled, delivered, acted, and dismissed times.
- Use enums in code for controlled values where possible.
- Inject `ApplicationDbContext` directly into endpoints or slice services for MVP work.
- Do not create generic repositories unless explicitly requested or strongly justified.
- Seed only stable system data: system categories, insight rules, and prompt templates.

### Backend Domain Mapping

Use singular entity names that map to the ERD concepts:

```text
User
UserFinancialProfile
UserAuthIdentity
Category
Activity
CashflowEntry
LineItem
SpendingPlan
SpendingPlanItem
SpendingPlanTemplate
SpendingPlanTemplateItem
InsightRule
UserInsight
PromptTemplate
UserPrompt
UserBehaviorMetric
```

Do not create entities based on frontend screens. Entities should represent durable business concepts.

### Backend Rules

- Validate input at endpoint boundaries.
- Do not trust client-side validation.
- Keep database access explicit and testable.
- Use the ASP.NET Core Options Pattern for typed settings.
- Keep option classes under `Common/Settings` for cross-cutting configuration or inside the owning infrastructure folder for integration-specific settings.
- Bind options from `IConfiguration`, validate them at startup when required, and inject `IOptions<T>`, `IOptionsSnapshot<T>`, or `IOptionsMonitor<T>` instead of reading raw configuration throughout feature code.
- Use PostgreSQL naming and types intentionally.
- Keep domain rules near the feature until reuse justifies moving them.
- Use Supabase JWT validation for auth.
- Store local users so domain objects can consistently reference `UserId`.
- Use Resend only for transactional email flows.
- Do not introduce Quartz, Redis, SMS, or AI services until requested.
- Do not log sensitive user data, access tokens, or full financial notes.

### Backend Testing

Use xUnit, FluentAssertions, and Testcontainers.

Prioritize tests for the core loop:

```text
Create spending plan -> Add spending plan items -> Create spending activity -> Add line items -> Generate insights
```

Also test:

- Income entry creation
- Expense entry creation
- Planned vs actual variance
- Need vs want classification
- Prompt lifecycle actions
- User ownership boundaries

## Frontend Agent Rules

Follow `.agents/references/frontend_project_structure.md` for frontend structure. That file is the current frontend source of truth.

### Frontend Feature Slices

Start with these MVP feature slices only:

```text
auth
onboarding
financial-profile
activities
spending-plans
cashflow
line-items
spending-plan-templates
insights
prompts
categories
```

Do not add inventory, family sharing, AI assistant, or advanced automation folders in MVP.

### Frontend Naming

- Use kebab-case for folders.
- Use PascalCase for React components.
- Use camelCase for hooks, stores, utilities, and functions.
- Use `.screen.tsx` for route-level screens.
- Use `.component.tsx` for reusable feature components when useful.
- Use `.sheet.tsx` for bottom sheet components.
- Use `.schema.ts` for Zod schemas.
- Use `.api.ts` for TanStack Query API wrappers.
- Use `.store.ts` for Zustand stores.
- Use `.types.ts` for feature-specific TypeScript types.

### Expo Router Standards

Route files under `app/` should stay thin. They should compose feature screens, not contain business logic.

Good:

```tsx
export { CreateSpendingPlanScreen as default } from '@/features/spending-plans/screens/CreateSpendingPlan.screen';
```

Avoid placing complex state, API calls, or form logic directly inside route files.

### Frontend State and API Rules

- Use TanStack Query for server/cache state.
- Use Zustand for draft state, filters, modals, prompt UI state, and local UI state.
- Use React Hook Form for forms.
- Use Zod for validation schemas.
- Use feature-local API files such as `features/spending-plans/api/spending-plans.api.ts`.
- Put global HTTP behavior in `src/lib/api`.
- Avoid one giant global API service.
- Avoid duplicating server data into Zustand unless there is a clear draft or offline reason.

### Frontend UI Rules

- Use Tamagui for layout and reusable UI primitives.
- Keep feature-specific components inside their feature slice.
- Shared UI wrappers belong in `src/shared/ui`.
- Use FlashList for long lists such as cashflow timelines, activity history, spending plan item history, and insights feeds.
- Use bottom sheets for quick actions and spending prompts.
- Use MMKV for local drafts, recent templates, user preferences, and lightweight UI state.
- Use SecureStore for sensitive tokens or secure values.
- Use date-fns for date grouping and recurring helpers.
- Use Skia only for custom charts or visual summaries.
- Keep UI interactions fast enough for quick logging.

## Product Behavior Rules

### Core User Actions

The app should optimize these actions:

1. Create a spending plan before spending.
2. Reuse a common template.
3. Start or complete a spending activity.
4. Record actual cashflow and line items.
5. Compare planned vs actual spending.
6. Show a useful insight or prompt for the next cycle.

### Key Product Metric

Primary metric:

```text
% of users who create a spending plan before spending
```

Secondary metrics:

- Time to create a spending plan
- Repeat usage per spending cycle
- Planned vs actual variance
- Ratio of need vs want items
- Number of unplanned purchases
- Impulse purchase count and total
- Neglected essential count

### Prompt Philosophy

Prompts should be helpful, timely, and easy to dismiss.

Good prompt examples:

- "Planning groceries today?"
- "Reuse your last grocery list?"
- "You spent more than planned last time. Want to plan this one?"
- "How did this activity compare with your plan?"

Avoid guilt-heavy, spammy, or overly frequent prompts.

## Domain Language

Use consistent naming across backend, frontend, database, and UI.

### Core Terms

- `SpendingPlan`: A planned to-buy list before spending.
- `SpendingPlanItem`: An item inside a spending plan.
- `SpendingPlanTemplate`: A reusable spending plan structure.
- `SpendingPlanTemplateItem`: An item inside a reusable template.
- `Activity`: A real-world spending event or planned spending moment.
- `CashflowEntry`: A money movement, either income or expense.
- `LineItem`: Itemized detail for actual spending or income breakdowns.
- `Category`: System or user-created classification for cashflow, line items, and planned items.
- `Insight`: A rule-based reflection or summary.
- `Prompt`: A behavioral nudge shown to the user.
- `BehaviorMetric`: A period summary of planning and spending behavior.

Do not use `Inventory` for MVP concepts.

## Working Protocol for AI Coding Agents

Before making changes:

1. Read this `AGENTS.md` file.
2. Read the relevant required reference file in `.agents/references/`.
3. Use `.ai-workspace/` for scratch, planning, approval, worktree, and AI-agent task artifacts.
4. Inspect existing files before proposing or editing code.
5. Prefer small, coherent changes over large rewrites.
6. Preserve vertical slice boundaries.
7. Do not invent dependencies without checking the stack.
8. Do not add features outside MVP scope.
9. Update related tests or add tests when behavior changes.
10. Update documentation when architecture or product behavior changes.

## Definition of Done

A task is done only when:

- The change supports the MVP loop or requested scope.
- The code follows the required backend or frontend project structure.
- The data model respects the ERD context.
- Validation exists at the boundary.
- Errors are handled predictably.
- Tests are added or updated where needed.
- The UI remains fast and low-friction.
- Documentation is updated if the change affects structure or behavior.

## Final Reminder

This project wins through speed, timing, and reflection. Build fewer features, but make the core loop feel sharp.

```text
Plan -> Spend -> Compare -> Reflect -> Repeat
```
