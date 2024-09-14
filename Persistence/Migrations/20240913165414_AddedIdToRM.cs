using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedIdToRM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequirementManagements",
                table: "RequirementManagements");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RequirementManagements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequirementManagements",
                table: "RequirementManagements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RequirementManagements_RequirementId",
                table: "RequirementManagements",
                column: "RequirementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequirementManagements",
                table: "RequirementManagements");

            migrationBuilder.DropIndex(
                name: "IX_RequirementManagements_RequirementId",
                table: "RequirementManagements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RequirementManagements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequirementManagements",
                table: "RequirementManagements",
                columns: new[] { "RequirementId", "AssigneeId" });
        }
    }
}
