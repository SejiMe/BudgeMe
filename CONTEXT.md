# BudgeMe

BudgeMe is a pre-spending decision context focused on helping users plan purchases before spending, record what actually happened, compare the two, and improve the next cycle.

## Language

**SpendingPlan**:
A planned to-buy list created before a spending moment.
_Avoid_: Shopping list, checklist, inventory

**SpendingPlanItem**:
An item inside a **SpendingPlan** that represents intended spending.
_Avoid_: Inventory item, product

**SpendingPlanTemplate**:
A reusable structure for creating repeat **SpendingPlans**.
_Avoid_: Automation, recurring purchase

**SpendingPlanTemplateItem**:
An item inside a **SpendingPlanTemplate** that can become a **SpendingPlanItem**.
_Avoid_: Inventory item, saved product

**Activity**:
A real-world spending event or planned spending moment.
_Avoid_: Transaction, purchase

**CashflowEntry**:
A recorded money movement, either income or expense.
_Avoid_: Transaction

**LineItem**:
An itemized detail of actual spending or income breakdown.
_Avoid_: Spending plan item, inventory item

**Category**:
A system or user-created classification for planned items, actual items, and cashflow.
_Avoid_: Tag

**SystemCategory**:
A stable built-in **Category** available to every user.
_Avoid_: Default tag

**Insight**:
A rule-based reflection or summary generated from user planning and spending behavior.
_Avoid_: AI advice, recommendation

**InsightRule**:
A stable deterministic rule that powers one kind of **Insight**.
_Avoid_: AI model, recommendation engine

**Prompt**:
A behavioral nudge shown to the user.
_Avoid_: Reminder, notification, automation

**PromptTemplate**:
A stable built-in message pattern used to create a **Prompt**.
_Avoid_: Notification template, automation template

**BehaviorMetric**:
A period summary of planning and spending behavior.
_Avoid_: Analytics event

## Relationships

- A **User** owns zero or more **Activities**, **SpendingPlans**, **CashflowEntries**, **LineItems**, **SpendingPlanTemplates**, **Insights**, **Prompts**, and **BehaviorMetrics**.
- An **Activity** is created before its **SpendingPlan** in the core MVP flow.
- An **Activity** may have one **SpendingPlan**.
- Every MVP **SpendingPlan** belongs to exactly one **Activity**.
- A **SpendingPlan** contains one or more **SpendingPlanItems**.
- A **CashflowEntry** may have one or more **LineItems**.
- A **LineItem** may match one **SpendingPlanItem**.
- A **SpendingPlanTemplate** contains one or more **SpendingPlanTemplateItems** and may create **SpendingPlans**.
- **Insights** are powered by stable **InsightRules**.
- **Prompts** are generated from stable **PromptTemplates**.
- MVP **SystemCategories** are groceries, bills, transport, food-drink, household, health, personal-care, shopping, social, income, and other.
- MVP **InsightRules** are planned-over-actual, actual-over-planned, unplanned-purchase-detected, want-heavy-activity, and neglected-essential.
- MVP **PromptTemplates** are plan-activity, reuse-template, reflect-after-activity, and plan-after-overspend.

## Example Dialogue

> **Dev:** "When a user is planning groceries, do we create a CashflowEntry immediately?"
> **Domain expert:** "No. First they create a SpendingPlan for the planned Activity. CashflowEntry and LineItems record what happened after spending."

## Flagged Ambiguities

- "Transaction" is avoided because it can mean **CashflowEntry**, **LineItem**, or a bank integration event. MVP language uses **CashflowEntry** for money movement and **LineItem** for itemized details.
- "Reminder" is avoided for product nudges. MVP language uses **Prompt**, because delivery may be in-app and dismissible rather than scheduled notification automation.
- The core planning flow starts with an **Activity**. A **SpendingPlan** does not exist independently in MVP; it is created for an **Activity**.
