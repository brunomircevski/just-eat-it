using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEI.Migrations
{
    public partial class UserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Activity",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "Heigth",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsMan",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "Target",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Users",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Heigth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsMan",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Users");
        }
    }
}
