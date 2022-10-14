using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DAL.Migrations
{
    public partial class CreatedInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new Exception("Migration Error");
            }

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Skills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserIdCreate",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Materials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserIdCreate",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserIdCreate",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new Exception("Migration Error");
            }

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Courses");
        }
    }
}
