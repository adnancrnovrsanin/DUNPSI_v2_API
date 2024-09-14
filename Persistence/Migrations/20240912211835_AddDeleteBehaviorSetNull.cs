using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteBehaviorSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManagers_AspNetUsers_AppUserId",
                table: "ProductManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_AspNetUsers_AppUserId",
                table: "ProjectManagers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectManagers_AppUserId",
                table: "ProjectManagers");

            migrationBuilder.DropIndex(
                name: "IX_ProductManagers_AppUserId",
                table: "ProductManagers");

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
                values: new object[] { "401864a8-b24f-447a-9b23-2a942699cfbe", 0, "102233ae-af97-450d-b8a9-b04d60ef5eaa", "admin@admin.com", true, new DateTime(2024, 9, 12, 21, 18, 34, 54, DateTimeKind.Utc).AddTicks(7669), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEN43Y6+Mn7fc5UZXCY1zm5GE4ZzHWs7Md6tIymaHFS6clpWpKxmGo9gKikHZHpKfug==", null, false, 0, "d7294b16-fefc-4f2a-8853-cbfba199b8ea", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("a6b6be93-cd52-42bf-9e4f-f6646004709d"), "401864a8-b24f-447a-9b23-2a942699cfbe" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManagers_AppUserId",
                table: "ProjectManagers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductManagers_AppUserId",
                table: "ProductManagers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManagers_AspNetUsers_AppUserId",
                table: "ProductManagers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_AspNetUsers_AppUserId",
                table: "ProjectManagers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManagers_AspNetUsers_AppUserId",
                table: "ProductManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectManagers_AspNetUsers_AppUserId",
                table: "ProjectManagers");

            migrationBuilder.DropIndex(
                name: "IX_ProjectManagers_AppUserId",
                table: "ProjectManagers");

            migrationBuilder.DropIndex(
                name: "IX_ProductManagers_AppUserId",
                table: "ProductManagers");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("a6b6be93-cd52-42bf-9e4f-f6646004709d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "401864a8-b24f-447a-9b23-2a942699cfbe");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "25cf89fa-2b82-4e84-ba9a-2fedc7247c58", 0, "a44e46a5-16b0-433a-ae77-1adedc9a6fb2", "admin@admin.com", true, new DateTime(2024, 9, 12, 21, 13, 24, 807, DateTimeKind.Utc).AddTicks(7419), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAEJnG69pnqTvtxLsM93Peca5mKS9frkoEzAgJaeecv3m7oj0gN4AJZb2wePnshHu71w==", null, false, 0, "0f1c29aa-3eba-4a2c-960d-a746660fb17c", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("4aafc518-612c-492d-b1a3-83657f20f961"), "25cf89fa-2b82-4e84-ba9a-2fedc7247c58" });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectManagers_AppUserId",
                table: "ProjectManagers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductManagers_AppUserId",
                table: "ProductManagers",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManagers_AspNetUsers_AppUserId",
                table: "ProductManagers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectManagers_AspNetUsers_AppUserId",
                table: "ProjectManagers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
