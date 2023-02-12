using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.DataEF.Data.Migrations
{
    public partial class InitDB6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "ProjectComments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ProjectComments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "ProjectComments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLike",
                table: "ProjectComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                table: "ProjectComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProjectComments",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "level",
                table: "ProjectComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectComments_UserId",
                table: "ProjectComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectComments_AspNetUsers_UserId",
                table: "ProjectComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectComments_AspNetUsers_UserId",
                table: "ProjectComments");

            migrationBuilder.DropIndex(
                name: "IX_ProjectComments_UserId",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "NumberOfLike",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "ParentID",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectComments");

            migrationBuilder.DropColumn(
                name: "level",
                table: "ProjectComments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ProjectComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
