using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PreSpend.Api.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "insight_rules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rule_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    rule_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_insight_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "prompt_templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    trigger_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    prompt_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    message_template = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_prompt_templates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    avatar_url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    currency_code = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    income_level = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    family_support_flag = table.Column<bool>(type: "boolean", nullable: false),
                    onboarding_completed = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "activities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activity_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    trigger_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    planned_date = table.Column<DateOnly>(type: "date", nullable: true),
                    actual_date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    planned_budget = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    emotional_context = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activities", x => x.id);
                    table.ForeignKey(
                        name: "fk_activities_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    category_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_system_default = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_categories_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spending_plan_templates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    activity_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_system_default = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spending_plan_templates", x => x.id);
                    table.ForeignKey(
                        name: "fk_spending_plan_templates_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_auth_identities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    provider_user_id = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    provider_email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    last_login_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_auth_identities", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_auth_identities_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_behavior_metrics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    period_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    period_start = table.Column<DateOnly>(type: "date", nullable: false),
                    period_end = table.Column<DateOnly>(type: "date", nullable: false),
                    audits_created_count = table.Column<int>(type: "integer", nullable: false),
                    activities_completed_count = table.Column<int>(type: "integer", nullable: false),
                    planned_spending_total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    actual_spending_total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    impulse_purchase_count = table.Column<int>(type: "integer", nullable: false),
                    impulse_purchase_total = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    neglected_essential_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_behavior_metrics", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_behavior_metrics_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_financial_profiles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    income_frequency = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    next_expected_income_date = table.Column<DateOnly>(type: "date", nullable: true),
                    payday_day_of_month = table.Column<int>(type: "integer", nullable: true),
                    timezone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_financial_profiles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_financial_profiles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_insights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rule_id = table.Column<Guid>(type: "uuid", nullable: false),
                    insight_title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    insight_message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    severity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    metadata = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    seen_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_insights", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_insights_activities_activity_id",
                        column: x => x.activity_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_insights_insight_rules_rule_id",
                        column: x => x.rule_id,
                        principalTable: "insight_rules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_insights_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_prompts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prompt_template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    related_activity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prompt_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    scheduled_for = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    delivered_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    seen_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    acted_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    dismissed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    action_taken = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    metadata = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_prompts", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_prompts_activities_related_activity_id",
                        column: x => x.related_activity_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_prompts_prompt_templates_prompt_template_id",
                        column: x => x.prompt_template_id,
                        principalTable: "prompt_templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_prompts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cashflow_entries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    entry_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    income_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    expense_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    source_or_payee = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    entry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cashflow_entries", x => x.id);
                    table.ForeignKey(
                        name: "fk_cashflow_entries_activities_activity_id",
                        column: x => x.activity_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_cashflow_entries_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cashflow_entries_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spending_plan_template_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    item_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    classification = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    priority_level = table.Column<int>(type: "integer", nullable: false),
                    estimated_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spending_plan_template_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_spending_plan_template_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_spending_plan_template_items_spending_plan_templates_templa",
                        column: x => x.template_id,
                        principalTable: "spending_plan_templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spending_plans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_from_template_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spending_plans", x => x.id);
                    table.ForeignKey(
                        name: "fk_spending_plans_activities_activity_id",
                        column: x => x.activity_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_spending_plans_spending_plan_templates_created_from_templat",
                        column: x => x.created_from_template_id,
                        principalTable: "spending_plan_templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_spending_plans_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "spending_plan_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    spending_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    item_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    classification = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    priority_level = table.Column<int>(type: "integer", nullable: false),
                    planned_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_spending_plan_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_spending_plan_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_spending_plan_items_spending_plans_spending_plan_id",
                        column: x => x.spending_plan_id,
                        principalTable: "spending_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "line_items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    activity_id = table.Column<Guid>(type: "uuid", nullable: true),
                    cashflow_entry_id = table.Column<Guid>(type: "uuid", nullable: true),
                    spending_plan_item_id = table.Column<Guid>(type: "uuid", nullable: true),
                    line_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    item_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    was_planned = table.Column<bool>(type: "boolean", nullable: false),
                    classification = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    note = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    occurred_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_line_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_line_items_activities_activity_id",
                        column: x => x.activity_id,
                        principalTable: "activities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_line_items_cashflow_entries_cashflow_entry_id",
                        column: x => x.cashflow_entry_id,
                        principalTable: "cashflow_entries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_line_items_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_line_items_spending_plan_items_spending_plan_item_id",
                        column: x => x.spending_plan_item_id,
                        principalTable: "spending_plan_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_line_items_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "category_type", "created_at", "is_system_default", "name", "updated_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111001"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "groceries", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111002"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "bills", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111003"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "transport", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111004"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "food-drink", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111005"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "household", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111006"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "health", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111007"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "personal-care", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111008"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "shopping", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111009"), "Expense", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "social", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111010"), "Income", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "income", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null },
                    { new Guid("11111111-1111-1111-1111-111111111011"), "Mixed", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "other", new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null }
                });

            migrationBuilder.InsertData(
                table: "insight_rules",
                columns: new[] { "id", "description", "is_active", "rule_code", "rule_name", "severity" },
                values: new object[,]
                {
                    { new Guid("22222222-2222-2222-2222-222222222001"), "Highlights activities where actual spend stayed below the planned budget.", true, "planned_over_actual", "Planned under actual spend", "Positive" },
                    { new Guid("22222222-2222-2222-2222-222222222002"), "Highlights activities where actual spend exceeded the planned budget.", true, "actual_over_planned", "Actual over planned spend", "Warning" },
                    { new Guid("22222222-2222-2222-2222-222222222003"), "Highlights actual line items that were not part of the spending plan.", true, "unplanned_purchase_detected", "Unplanned purchase detected", "Warning" },
                    { new Guid("22222222-2222-2222-2222-222222222004"), "Highlights completed activities where want-classified items outweighed need-classified items.", true, "want_heavy_activity", "Want-heavy activity", "Info" },
                    { new Guid("22222222-2222-2222-2222-222222222005"), "Highlights need-classified planned items that were not purchased during the activity.", true, "neglected_essential", "Neglected essential", "Info" }
                });

            migrationBuilder.InsertData(
                table: "prompt_templates",
                columns: new[] { "id", "created_at", "is_active", "message_template", "prompt_type", "severity", "trigger_code" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333001"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "Planning {activityType} today?", "PlanActivity", "Info", "plan_activity" },
                    { new Guid("33333333-3333-3333-3333-333333333002"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "Reuse your last {activityType} plan?", "ReuseTemplate", "Info", "reuse_template" },
                    { new Guid("33333333-3333-3333-3333-333333333003"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "How did this activity compare with your plan?", "ReflectAfterActivity", "Info", "reflect_after_activity" },
                    { new Guid("33333333-3333-3333-3333-333333333004"), new DateTimeOffset(new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), true, "You spent more than planned last time. Want to plan this one?", "PlanAfterOverspend", "Warning", "plan_after_overspend" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_activities_user_id_planned_date",
                table: "activities",
                columns: new[] { "user_id", "planned_date" });

            migrationBuilder.CreateIndex(
                name: "ix_cashflow_entries_activity_id",
                table: "cashflow_entries",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_cashflow_entries_category_id",
                table: "cashflow_entries",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_cashflow_entries_user_id_entry_date",
                table: "cashflow_entries",
                columns: new[] { "user_id", "entry_date" });

            migrationBuilder.CreateIndex(
                name: "ix_categories_user_id_name",
                table: "categories",
                columns: new[] { "user_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_insight_rules_rule_code",
                table: "insight_rules",
                column: "rule_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_line_items_activity_id",
                table: "line_items",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_line_items_cashflow_entry_id",
                table: "line_items",
                column: "cashflow_entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_line_items_category_id",
                table: "line_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_line_items_spending_plan_item_id",
                table: "line_items",
                column: "spending_plan_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_line_items_user_id_occurred_at",
                table: "line_items",
                columns: new[] { "user_id", "occurred_at" });

            migrationBuilder.CreateIndex(
                name: "ix_prompt_templates_trigger_code",
                table: "prompt_templates",
                column: "trigger_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_spending_plan_items_category_id",
                table: "spending_plan_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_spending_plan_items_spending_plan_id",
                table: "spending_plan_items",
                column: "spending_plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_spending_plan_template_items_category_id",
                table: "spending_plan_template_items",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_spending_plan_template_items_template_id",
                table: "spending_plan_template_items",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_spending_plan_templates_user_id",
                table: "spending_plan_templates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_spending_plans_activity_id",
                table: "spending_plans",
                column: "activity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_spending_plans_created_from_template_id",
                table: "spending_plans",
                column: "created_from_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_spending_plans_user_id",
                table: "spending_plans",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_auth_identities_provider_provider_user_id",
                table: "user_auth_identities",
                columns: new[] { "provider", "provider_user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_auth_identities_user_id",
                table: "user_auth_identities",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_behavior_metrics_user_id_period_type_period_start_peri",
                table: "user_behavior_metrics",
                columns: new[] { "user_id", "period_type", "period_start", "period_end" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_financial_profiles_user_id",
                table: "user_financial_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_insights_activity_id",
                table: "user_insights",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_insights_rule_id",
                table: "user_insights",
                column: "rule_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_insights_user_id",
                table: "user_insights",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_prompts_prompt_template_id",
                table: "user_prompts",
                column: "prompt_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_prompts_related_activity_id",
                table: "user_prompts",
                column: "related_activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_prompts_user_id_status",
                table: "user_prompts",
                columns: new[] { "user_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "line_items");

            migrationBuilder.DropTable(
                name: "spending_plan_template_items");

            migrationBuilder.DropTable(
                name: "user_auth_identities");

            migrationBuilder.DropTable(
                name: "user_behavior_metrics");

            migrationBuilder.DropTable(
                name: "user_financial_profiles");

            migrationBuilder.DropTable(
                name: "user_insights");

            migrationBuilder.DropTable(
                name: "user_prompts");

            migrationBuilder.DropTable(
                name: "cashflow_entries");

            migrationBuilder.DropTable(
                name: "spending_plan_items");

            migrationBuilder.DropTable(
                name: "insight_rules");

            migrationBuilder.DropTable(
                name: "prompt_templates");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "spending_plans");

            migrationBuilder.DropTable(
                name: "activities");

            migrationBuilder.DropTable(
                name: "spending_plan_templates");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
