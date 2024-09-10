using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingNullableToInitialRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitialProjectRequests_ProjectManagers_AppointedManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointedManagerId",
                table: "InitialProjectRequests",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_InitialProjectRequests_ProjectManagers_AppointedManagerId",
                table: "InitialProjectRequests",
                column: "AppointedManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InitialProjectRequests_ProjectManagers_AppointedManagerId",
                table: "InitialProjectRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppointedManagerId",
                table: "InitialProjectRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectManagerId",
                table: "InitialProjectRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_InitialProjectRequests_ProjectManagers_AppointedManagerId",
                table: "InitialProjectRequests",
                column: "AppointedManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
