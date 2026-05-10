# Frontend Project Structure

## Purpose

This document defines the recommended frontend project structure for the mobile-first pre-spending decision platform. The project uses Expo, React Native, Expo Router, Tamagui, TanStack Query, Zustand, FlashList, React Hook Form, Zod, Expo notifications, MMKV, and related mobile tooling.

The structure follows Vertical Slice Architecture principles: each product feature owns its screens, components, hooks, API calls, validation schemas, state, and feature-specific types where practical.

## Core Product Flow

The frontend should be organized around the product loop:

1. Income or payday context
2. Create a spending plan
3. Start or complete a spending activity
4. Record cashflow and line items
5. Show insights and prompts

This means the app should not be organized only by technical layers such as `components`, `hooks`, and `services`. Shared technical folders are allowed, but feature behavior should live close together.

## Naming Convention

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
- Keep feature-specific code inside the feature folder unless it is truly shared.

Examples:

```text
CreateSpendingPlan.screen.tsx
SpendingPlanItemRow.component.tsx
SpendingPlanQuickAdd.sheet.tsx
spending-plan.schema.ts
spending-plan.api.ts
spending-plan.store.ts
spending-plan.types.ts
```

## Recommended Project Structure

```ascii
app/
+--- _layout.tsx                                  # Root Expo Router layout
+--- index.tsx                                    # App entry route / redirect logic
+--- (auth)/                                      # Auth route group
|    +--- _layout.tsx
|    +--- sign-in.screen.tsx
|    +--- auth-callback.screen.tsx
+--- (onboarding)/                               # First-run setup route group
|    +--- _layout.tsx
|    +--- financial-profile.screen.tsx
|    +--- preferences.screen.tsx
+--- (tabs)/                                     # Main app shell
|    +--- _layout.tsx
|    +--- home.screen.tsx
|    +--- activities.screen.tsx
|    +--- cashflow.screen.tsx
|    +--- insights.screen.tsx
|    +--- settings.screen.tsx
+--- activities/
|    +--- [activityId].screen.tsx                 # Activity detail route
|    +--- create.screen.tsx                       # Create planned/on-the-spot activity
+--- spending-plans/
|    +--- [spendingPlanId].screen.tsx             # Spending plan detail route
|    +--- create.screen.tsx                       # Create spending plan
+--- spending-plan-templates/
|    +--- [templateId].screen.tsx
|    +--- create.screen.tsx
+--- prompts/
|    +--- [promptId].screen.tsx

src/
+--- app/                                        # App bootstrap and providers
|    +--- providers/
|    |    +--- AppProviders.tsx                  # QueryClient, Tamagui, auth, safe area, etc.
|    |    +--- QueryProvider.tsx
|    |    +--- TamaguiProvider.tsx
|    |    +--- AuthSessionProvider.tsx
|    +--- config/
|    |    +--- env.ts                            # Environment validation
|    |    +--- query-client.ts
|    |    +--- routes.ts
|    +--- navigation/
|         +--- linking.ts                        # Deep link handling
|
+--- features/                                   # Vertical slices live here
|    +--- auth/
|    |    +--- api/auth.api.ts
|    |    +--- components/GoogleSignInButton.component.tsx
|    |    +--- hooks/useAuthSession.ts
|    |    +--- hooks/useRequireAuth.ts
|    |    +--- stores/auth.store.ts
|    |    +--- schemas/auth.schema.ts
|    |    +--- types/auth.types.ts
|    |
|    +--- onboarding/
|    |    +--- api/onboarding.api.ts
|    |    +--- components/IncomeFrequencyPicker.component.tsx
|    |    +--- components/FamilySupportToggle.component.tsx
|    |    +--- hooks/useCompleteOnboarding.ts
|    |    +--- schemas/onboarding.schema.ts
|    |    +--- types/onboarding.types.ts
|    |
|    +--- financial-profile/
|    |    +--- api/financial-profile.api.ts
|    |    +--- components/PaydayCard.component.tsx
|    |    +--- components/IncomeScheduleForm.component.tsx
|    |    +--- hooks/useFinancialProfile.ts
|    |    +--- schemas/financial-profile.schema.ts
|    |    +--- types/financial-profile.types.ts
|    |
|    +--- activities/
|    |    +--- api/activities.api.ts
|    |    +--- components/ActivityCard.component.tsx
|    |    +--- components/ActivityStatusBadge.component.tsx
|    |    +--- components/ActivityTypePicker.component.tsx
|    |    +--- components/ActivityTimeline.component.tsx
|    |    +--- sheets/StartActivity.sheet.tsx
|    |    +--- sheets/CompleteActivity.sheet.tsx
|    |    +--- hooks/useActivities.ts
|    |    +--- hooks/useActivityDetail.ts
|    |    +--- hooks/useCreateActivity.ts
|    |    +--- stores/activity-draft.store.ts
|    |    +--- schemas/activity.schema.ts
|    |    +--- types/activity.types.ts
|    |
|    +--- spending-plans/
|    |    +--- api/spending-plans.api.ts
|    |    +--- components/SpendingPlanCard.component.tsx
|    |    +--- components/SpendingPlanItemRow.component.tsx
|    |    +--- components/NeedWantPicker.component.tsx
|    |    +--- components/PriorityPicker.component.tsx
|    |    +--- sheets/SpendingPlanQuickAdd.sheet.tsx
|    |    +--- sheets/ReuseLastSpendingPlan.sheet.tsx
|    |    +--- hooks/useSpendingPlans.ts
|    |    +--- hooks/useCreateSpendingPlan.ts
|    |    +--- hooks/useSpendingPlanItems.ts
|    |    +--- stores/spending-plan-draft.store.ts
|    |    +--- schemas/spending-plan.schema.ts
|    |    +--- types/spending-plan.types.ts
|    |
|    +--- cashflow/
|    |    +--- api/cashflow.api.ts
|    |    +--- components/CashflowEntryCard.component.tsx
|    |    +--- components/CashflowTimeline.component.tsx
|    |    +--- components/IncomeExpenseToggle.component.tsx
|    |    +--- sheets/QuickExpense.sheet.tsx
|    |    +--- sheets/QuickIncome.sheet.tsx
|    |    +--- hooks/useCashflowEntries.ts
|    |    +--- hooks/useCreateCashflowEntry.ts
|    |    +--- stores/cashflow-filter.store.ts
|    |    +--- schemas/cashflow.schema.ts
|    |    +--- types/cashflow.types.ts
|    |
|    +--- line-items/
|    |    +--- api/line-items.api.ts
|    |    +--- components/LineItemRow.component.tsx
|    |    +--- components/LineItemEditor.component.tsx
|    |    +--- components/PlannedMatchBadge.component.tsx
|    |    +--- hooks/useLineItems.ts
|    |    +--- hooks/useCreateLineItem.ts
|    |    +--- schemas/line-item.schema.ts
|    |    +--- types/line-item.types.ts
|    |
|    +--- spending-plan-templates/
|    |    +--- api/spending-plan-templates.api.ts
|    |    +--- components/SpendingPlanTemplateCard.component.tsx
|    |    +--- components/SpendingPlanTemplateItemRow.component.tsx
|    |    +--- hooks/useSpendingPlanTemplates.ts
|    |    +--- hooks/useCreateSpendingPlanFromTemplate.ts
|    |    +--- stores/template-draft.store.ts
|    |    +--- schemas/spending-plan-template.schema.ts
|    |    +--- types/spending-plan-template.types.ts
|    |
|    +--- insights/
|    |    +--- api/insights.api.ts
|    |    +--- components/InsightCard.component.tsx
|    |    +--- components/PlannedVsActualChart.component.tsx
|    |    +--- components/WantsVsNeedsRing.component.tsx
|    |    +--- hooks/useInsights.ts
|    |    +--- hooks/useActivityInsights.ts
|    |    +--- types/insight.types.ts
|    |
|    +--- prompts/
|    |    +--- api/prompts.api.ts
|    |    +--- components/PromptCard.component.tsx
|    |    +--- components/PaydayPrompt.component.tsx
|    |    +--- components/ReflectionPrompt.component.tsx
|    |    +--- hooks/usePrompts.ts
|    |    +--- hooks/usePromptActions.ts
|    |    +--- stores/prompt-ui.store.ts
|    |    +--- types/prompt.types.ts
|    |
|    +--- categories/
|         +--- api/categories.api.ts
|         +--- components/CategoryPicker.component.tsx
|         +--- hooks/useCategories.ts
|         +--- types/category.types.ts
|
+--- shared/                                     # Shared, non-feature-specific code only
|    +--- ui/
|    |    +--- AppButton.tsx
|    |    +--- AppCard.tsx
|    |    +--- AppTextField.tsx
|    |    +--- AppEmptyState.tsx
|    |    +--- AppScreen.tsx
|    +--- layout/
|    |    +--- ScreenHeader.tsx
|    |    +--- SectionHeader.tsx
|    +--- feedback/
|    |    +--- Toast.tsx
|    |    +--- ErrorState.tsx
|    |    +--- LoadingState.tsx
|    +--- forms/
|    |    +--- FormField.tsx
|    |    +--- MoneyInput.tsx
|    |    +--- DatePickerField.tsx
|    +--- hooks/
|    |    +--- useDebouncedValue.ts
|    |    +--- useHaptics.ts
|    +--- utils/
|    |    +--- money.ts
|    |    +--- dates.ts
|    |    +--- errors.ts
|    |    +--- format.ts
|    +--- types/
|         +--- api.types.ts
|         +--- pagination.types.ts
|
+--- lib/                                        # Technical integrations
|    +--- api/
|    |    +--- http-client.ts
|    |    +--- query-keys.ts
|    |    +--- api-error.ts
|    +--- auth/
|    |    +--- supabase-client.ts
|    |    +--- secure-session.ts
|    +--- storage/
|    |    +--- mmkv.ts
|    |    +--- secure-store.ts
|    +--- notifications/
|    |    +--- notification-client.ts
|    |    +--- notification-permissions.ts
|    +--- analytics/
|    |    +--- analytics-client.ts
|    +--- validation/
|         +--- zod-error-map.ts
|
+--- theme/
|    +--- tamagui.config.ts
|    +--- tokens.ts
|    +--- fonts.ts
|    +--- colors.ts
|
+--- assets/
|    +--- images/
|    +--- icons/
|    +--- mascot/
|
+--- tests/
|    +--- setup.ts
|    +--- mocks/
|    +--- factories/
|
+--- e2e/
|    +--- maestro/
|         +--- auth-flow.yaml
|         +--- create-spending-plan-flow.yaml
|         +--- complete-activity-flow.yaml
|         +--- quick-expense-flow.yaml
```

## Feature Slice Rules

Each feature folder should own the code needed to complete that feature's user behavior.

A typical slice may contain:

```ascii
features/{feature-name}/
+--- api/                 # TanStack Query functions, mutations, request/response mappers
+--- components/          # Feature-only UI components
+--- sheets/              # Feature-specific bottom sheets
+--- hooks/               # Feature-specific hooks
+--- stores/              # Feature-local Zustand stores, usually for drafts or UI state
+--- schemas/             # Zod schemas
+--- types/               # Feature-specific TypeScript types
+--- utils/               # Feature-only helpers, only if needed
```

Do not create empty folders just to match the template. Add folders when the feature needs them.

## Route Ownership

Expo Router files under `app/` should stay thin. They should mostly compose feature screens or feature components from `src/features`.

Example:

```tsx
// app/spending-plans/create.screen.tsx
export { CreateSpendingPlanScreen as default } from '@/features/spending-plans/screens/CreateSpendingPlan.screen';
```

If preferred, feature screen components may live under:

```text
src/features/spending-plans/screens/CreateSpendingPlan.screen.tsx
```

This keeps route files simple while keeping feature implementation inside the vertical slice.

## API Layer Rules

Use TanStack Query per feature. Avoid one giant global API service.

Good:

```text
features/spending-plans/api/spending-plans.api.ts
features/activities/api/activities.api.ts
features/cashflow/api/cashflow.api.ts
```

Avoid:

```text
services/api.ts
services/spendingPlanService.ts
services/activityService.ts
```

Each feature API file should expose:

- Query functions
- Mutation functions
- Query keys or feature-specific key helpers
- Request/response mappers if the backend DTO differs from UI shape

Global API concerns belong in:

```text
src/lib/api/http-client.ts
src/lib/api/query-keys.ts
src/lib/api/api-error.ts
```

## State Management Rules

Use the correct tool for each type of state:

- Server/cache state: TanStack Query
- Draft state: Zustand or React Hook Form
- Form state: React Hook Form
- Validation: Zod
- Auth/session persistence: Supabase + secure storage
- Fast local drafts/preferences: MMKV
- Pure component state: React `useState`

Avoid duplicating server data into Zustand unless there is a specific offline or draft-use case.

## Form Rules

Forms should use React Hook Form and Zod.

Recommended pattern:

```text
features/spending-plans/schemas/spending-plan.schema.ts
features/spending-plans/components/SpendingPlanItemEditor.component.tsx
features/spending-plans/hooks/useCreateSpendingPlan.ts
```

All money values should be validated as positive decimal numbers and formatted through shared money utilities.

## UI Rules

Use Tamagui for layout and reusable UI primitives. Shared UI wrappers should live in `src/shared/ui`.

Feature-specific components should remain inside their feature slice.

Example:

- `src/shared/ui/AppButton.tsx` is allowed.
- `src/features/spending-plans/components/SpendingPlanItemRow.component.tsx` should not be moved to shared unless multiple unrelated features truly use it.

## Performance Rules

Use FlashList for long lists such as:

- Transaction timelines
- Activity history
- Spending plan item history
- Insights feed

Keep list rows small and memoized when needed.

## Notification Rules

Prompt and reminder UI belongs to the `prompts` feature.

Expo notification setup belongs to:

```text
src/lib/notifications/
```

Do not mix product prompt logic with low-level notification permission setup.

## Local Storage Rules

Use secure storage only for sensitive values.

Use MMKV for:

- Draft spending plans
- Recent templates
- User preferences
- Last selected filters
- Lightweight cached UI state

Do not store raw secrets or long-lived access tokens in MMKV.

## Testing Structure

Use React Native Testing Library for component and hook tests. Use Maestro for critical end-to-end flows.

Recommended MVP E2E flows:

1. Sign in with Google
2. Complete onboarding
3. Create spending plan
4. Complete the linked activity
5. Add actual line items
6. View planned vs actual insight
7. Record random expense

## Vertical Slice Decision Rule

When adding a file, ask:

> Does this file belong to one product behavior, or is it truly shared?

If it belongs to one behavior, place it in `src/features/{feature-name}`.

If it is reused across multiple unrelated features, place it in `src/shared` or `src/lib`.

## MVP Feature Slices

Start with these slices only:

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

## Suggested Follow-Up Reference Files

This file may live at:

```text
.agents/references/frontend-project-structure.md
```

Related files may include:

```text
.agents/references/backend-api-project-structure.md
.agents/references/erd.md
.agents/references/product-requirements.md
.agents/references/frontend-stack.md
.agents/references/backend-stack.md
```

## Final Principle

The frontend should make the product loop feel fast:

> Plan before spending, record what happened, then reflect with useful insight.

Everything in the structure should support that loop without adding unnecessary ceremony.
