using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DataEF.Data.Migrations
{
    public partial class UploadedFile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProjectUploadedFiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ProjectUploadedFiles",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProjectUploadedFiles");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ProjectUploadedFiles");
        }
    }
}
