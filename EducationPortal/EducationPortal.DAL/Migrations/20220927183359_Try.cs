using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DAL.Migrations
{
    public partial class Try : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new Exception("migration");
            }

            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "UserIdCreate",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "UserNameCreate",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserNameCreate",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserNameCreate",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new Exception("migration");
            }

            migrationBuilder.DropColumn(
                name: "UserNameCreate",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "UserNameCreate",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "UserNameCreate",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "UserIdCreate",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserIdCreate",
                table: "Materials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserIdCreate",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
