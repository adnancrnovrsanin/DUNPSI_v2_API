using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeveloperDeleteBehaviorSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_AspNetUsers_AppUserId",
                table: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_AppUserId",
                table: "Developers");

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
                values: new object[] { "25cf89fa-2b82-4e84-ba9a-2fedc7247c58", 0, "a44e46a5-16b0-433a-ae77-1adedc9a6fb2", "admin@admin.com", true, new DateTime(2024, 9, 12, 21, 13, 24, 807, DateTimeKind.Utc).AddTicks(7419), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEJnG69pnqTvtxLsM93Peca5mKS9frkoEzAgJaeecv3m7oj0gN4AJZb2wePnshHu71w==", null, false, 0, "0f1c29aa-3eba-4a2c-960d-a746660fb17c", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("4aafc518-612c-492d-b1a3-83657f20f961"), "25cf89fa-2b82-4e84-ba9a-2fedc7247c58" });

            migrationBuilder.CreateIndex(
                name: "IX_Developers_AppUserId",
                table: "Developers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_AspNetUsers_AppUserId",
                table: "Developers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_AspNetUsers_AppUserId",
                table: "Developers");

            migrationBuilder.DropIndex(
                name: "IX_Developers_AppUserId",
                table: "Developers");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("4aafc518-612c-492d-b1a3-83657f20f961"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "25cf89fa-2b82-4e84-ba9a-2fedc7247c58");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ab76e09b-9407-4984-a7fc-a866db3eb34a", 0, "d6ad8298-3bdd-4acc-a69f-a8859fd6f9ab", "admin@admin.com", true, new DateTime(2024, 9, 12, 19, 28, 1, 544, DateTimeKind.Utc).AddTicks(6076), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEIchCCYXJ1gjClrNY0SJyjv73u35h8uZUUUfLs9OhVYbkm5fTra5XTv02hqw7QHRrw==", null, false, 0, "861deb54-8c76-42b7-87c6-1c1c578f2ce8", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("01507ed6-4ea9-4b7b-9a9b-df63853d7179"), "ab76e09b-9407-4984-a7fc-a866db3eb34a" });

            migrationBuilder.CreateIndex(
                name: "IX_Developers_AppUserId",
                table: "Developers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_AspNetUsers_AppUserId",
                table: "Developers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
