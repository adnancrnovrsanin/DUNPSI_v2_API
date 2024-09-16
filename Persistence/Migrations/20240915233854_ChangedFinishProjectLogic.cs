using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedFinishProjectLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("a6b6be93-cd52-42bf-9e4f-f6646004709d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "401864a8-b24f-447a-9b23-2a942699cfbe");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "SoftwareProjects");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SoftwareProjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6c38186e-7535-4c40-ac09-1e7bae1fa25e", 0, "5866ac94-5bd7-463c-a9c2-1dc0c7850037", "admin@admin.com", true, new DateTime(2024, 9, 15, 23, 38, 52, 504, DateTimeKind.Utc).AddTicks(9234), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAELCwHjFXSAeZDV/WIBWZYezrsGNyKDB8TaTokqbrezR3EAO8R8s/jxUuUop9NmAT0Q==", null, false, 0, "436c2640-683a-4d3e-a62e-f46701a3bcbf", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("6c79e61b-64c3-4f9e-93cb-4a58743405ab"), "6c38186e-7535-4c40-ac09-1e7bae1fa25e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("6c79e61b-64c3-4f9e-93cb-4a58743405ab"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c38186e-7535-4c40-ac09-1e7bae1fa25e");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SoftwareProjects");

            migrationBuilder.AddColumn<bool>(
                name: "Finished",
                table: "SoftwareProjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "401864a8-b24f-447a-9b23-2a942699cfbe", 0, "102233ae-af97-450d-b8a9-b04d60ef5eaa", "admin@admin.com", true, new DateTime(2024, 9, 12, 21, 18, 34, 54, DateTimeKind.Utc).AddTicks(7669), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEN43Y6+Mn7fc5UZXCY1zm5GE4ZzHWs7Md6tIymaHFS6clpWpKxmGo9gKikHZHpKfug==", null, false, 0, "d7294b16-fefc-4f2a-8853-cbfba199b8ea", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("a6b6be93-cd52-42bf-9e4f-f6646004709d"), "401864a8-b24f-447a-9b23-2a942699cfbe" });
        }
    }
}
