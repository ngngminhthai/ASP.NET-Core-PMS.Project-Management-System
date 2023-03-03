using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DataEF.Data.Migrations
{
    public partial class Con3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Messages",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
