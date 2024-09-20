using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImplementigRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Developers_DeveloperId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_DeveloperId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_RequirementId",
                table: "Ratings");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("e586ff92-e51c-4333-82be-84792a03e0fe"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8954e411-8318-4e04-99f9-ae06c127467d");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Ratings");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Requirements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Estimate",
                table: "Requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RequirementManagements",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "QualityRating",
                table: "Developers",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "Developers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "648be0e8-f17c-4df2-a94d-a9c2142d6c1c", 0, "c7be3a00-5b68-4af4-99ea-9371dde2865f", "admin@example.com", true, new DateTime(2024, 9, 18, 20, 32, 39, 445, DateTimeKind.Utc).AddTicks(8407), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAED6tHcLGkMMYmnMiaF0Yf+jd2ZrFD+E9sJvTcthJyQdEupSLJ361AfozDHbkbHHxfQ==", null, false, 0, "322a7967-5e74-4ef3-899b-11befda4ed16", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("51a08ca5-fc28-4b05-a317-cecb4c381b73"), "648be0e8-f17c-4df2-a94d-a9c2142d6c1c" });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RequirementId",
                table: "Ratings",
                column: "RequirementId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_RequirementId",
                table: "Ratings");

            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: new Guid("51a08ca5-fc28-4b05-a317-cecb4c381b73"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "648be0e8-f17c-4df2-a94d-a9c2142d6c1c");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RequirementManagements");

            migrationBuilder.DropColumn(
                name: "QualityRating",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "Developers");

            migrationBuilder.AddColumn<Guid>(
                name: "DeveloperId",
                table: "Ratings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastActive", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8954e411-8318-4e04-99f9-ae06c127467d", 0, "f925a7a8-e9c0-4c19-bdf5-06835b5f9a2e", "admin@example.com", true, new DateTime(2024, 9, 16, 17, 31, 29, 835, DateTimeKind.Utc).AddTicks(5922), false, null, "Admin", null, null, "AQAAAAIAAYagAAAAELD8zfRScPCmkvOkJIAlZUsy2CD6Twnbe2k0UkT283RGXB9Ll32P/Xl5PViAmILMHA==", null, false, 0, "1962dcf0-0a7e-4f67-a50c-5998dde0facd", "Admin", false, "admin" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "AppUserId" },
                values: new object[] { new Guid("e586ff92-e51c-4333-82be-84792a03e0fe"), "8954e411-8318-4e04-99f9-ae06c127467d" });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DeveloperId",
                table: "Ratings",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RequirementId",
                table: "Ratings",
                column: "RequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Developers_DeveloperId",
                table: "Ratings",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
