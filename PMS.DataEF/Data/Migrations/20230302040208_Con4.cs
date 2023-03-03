using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DataEF.Data.Migrations
{
    public partial class Con4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Type",
                table: "Conversations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Conversations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Conversations");
        }
    }
}
