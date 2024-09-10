using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ManagerCanRejectProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppointedManagerId",
                table: "InitialProjectRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ManagerRejectionReason",
                table: "InitialProjectRequests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectManagerId",
                table: "InitialProjectRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "RejectedByManager",
                table: "InitialProjectRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_InitialProjectRequests_AppointedManagerId",
                table: "InitialProjectRequests",
                column: "AppointedManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_InitialProjectRequests_ProjectManagers_AppointedManagerId",
                table: "InitialProjectRequests",
                column: "AppointedManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitialProjectRequests_ProjectManagers_AppointedManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.DropIndex(
                name: "IX_InitialProjectRequests_AppointedManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.DropColumn(
                name: "AppointedManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.DropColumn(
                name: "ManagerRejectionReason",
                table: "InitialProjectRequests");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.DropColumn(
                name: "RejectedByManager",
                table: "InitialProjectRequests");
        }
    }
}
