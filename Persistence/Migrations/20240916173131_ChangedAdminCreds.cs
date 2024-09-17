using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAdminCreds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("6c79e61b-64c3-4f9e-93cb-4a58743405ab"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c38186e-7535-4c40-ac09-1e7bae1fa25e");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8954e411-8318-4e04-99f9-ae06c127467d", 0, "f925a7a8-e9c0-4c19-bdf5-06835b5f9a2e", "admin@example.com", true, new DateTime(2024, 9, 16, 17, 31, 29, 835, DateTimeKind.Utc).AddTicks(5922), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAELD8zfRScPCmkvOkJIAlZUsy2CD6Twnbe2k0UkT283RGXB9Ll32P/Xl5PViAmILMHA==", null, false, 0, "1962dcf0-0a7e-4f67-a50c-5998dde0facd", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("e586ff92-e51c-4333-82be-84792a03e0fe"), "8954e411-8318-4e04-99f9-ae06c127467d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("e586ff92-e51c-4333-82be-84792a03e0fe"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8954e411-8318-4e04-99f9-ae06c127467d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6c38186e-7535-4c40-ac09-1e7bae1fa25e", 0, "5866ac94-5bd7-463c-a9c2-1dc0c7850037", "admin@admin.com", true, new DateTime(2024, 9, 15, 23, 38, 52, 504, DateTimeKind.Utc).AddTicks(9234), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAELCwHjFXSAeZDV/WIBWZYezrsGNyKDB8TaTokqbrezR3EAO8R8s/jxUuUop9NmAT0Q==", null, false, 0, "436c2640-683a-4d3e-a62e-f46701a3bcbf", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("6c79e61b-64c3-4f9e-93cb-4a58743405ab"), "6c38186e-7535-4c40-ac09-1e7bae1fa25e" });
        }
    }
}
