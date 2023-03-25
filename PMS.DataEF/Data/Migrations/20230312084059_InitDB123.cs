using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DataEF.Data.Migrations
{
    public partial class InitDB123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dependencies",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Successors",
                table: "ProjectTasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageProfile",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectTaskProjectTask",
                columns: table => new
                {
                    DependentTasksId = table.Column<int>(type: "int", nullable: false),
                    SuccessorTaksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTaskProjectTask", x => new { x.DependentTasksId, x.SuccessorTaksId });
                    table.ForeignKey(
                        name: "FK_ProjectTaskProjectTask_ProjectTasks_DependentTasksId",
                        column: x => x.DependentTasksId,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTaskProjectTask_ProjectTasks_SuccessorTaksId",
                        column: x => x.SuccessorTaksId,
                        principalTable: "ProjectTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTaskProjectTask_SuccessorTaksId",
                table: "ProjectTaskProjectTask",
                column: "SuccessorTaksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTaskProjectTask");

            migrationBuilder.DropColumn(
                name: "Dependencies",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "Successors",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "ImageProfile",
                table: "AspNetUsers");
        }
    }
}
