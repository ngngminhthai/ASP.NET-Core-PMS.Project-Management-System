using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DataEF.Data.Migrations
{
    public partial class WorkingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Priority_ProjectTasks_ProjectTaskId",
                table: "Priority");

            migrationBuilder.DropIndex(
                name: "IX_Priority_ProjectTaskId",
                table: "Priority");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                table: "Priority");

            migrationBuilder.AddColumn<int>(
                name: "WorkingStatusValue",
                table: "ProjectTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkingStatusValue",
                table: "ProjectTasks");

            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                table: "Priority",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Priority_ProjectTaskId",
                table: "Priority",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Priority_ProjectTasks_ProjectTaskId",
                table: "Priority",
                column: "ProjectTaskId",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
