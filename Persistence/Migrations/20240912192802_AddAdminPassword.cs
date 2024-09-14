using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("ae8250bf-038b-4f93-be07-2233a74f46eb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08644480-2ef2-4ed5-b76b-5b201fc62788");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab76e09b-9407-4984-a7fc-a866db3eb34a", 0, "d6ad8298-3bdd-4acc-a69f-a8859fd6f9ab", "admin@admin.com", true, new DateTime(2024, 9, 12, 19, 28, 1, 544, DateTimeKind.Utc).AddTicks(6076), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEIchCCYXJ1gjClrNY0SJyjv73u35h8uZUUUfLs9OhVYbkm5fTra5XTv02hqw7QHRrw==", null, false, 0, "861deb54-8c76-42b7-87c6-1c1c578f2ce8", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("01507ed6-4ea9-4b7b-9a9b-df63853d7179"), "ab76e09b-9407-4984-a7fc-a866db3eb34a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("01507ed6-4ea9-4b7b-9a9b-df63853d7179"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ab76e09b-9407-4984-a7fc-a866db3eb34a");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "08644480-2ef2-4ed5-b76b-5b201fc62788", 0, "f2bdfdf4-f951-4907-afb6-52e571957f91", "admin@admin.com", true, new DateTime(2024, 9, 12, 19, 16, 35, 696, DateTimeKind.Utc).AddTicks(6194), false, null, "Admin", null, null, null, null, false, 0, "0c2983d3-2aba-456c-85bb-dfc4b8ac388d", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("ae8250bf-038b-4f93-be07-2233a74f46eb"), "08644480-2ef2-4ed5-b76b-5b201fc62788" });
        }
    }
}
