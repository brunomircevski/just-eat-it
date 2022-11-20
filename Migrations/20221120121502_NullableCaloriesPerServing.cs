using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEI.Migrations
{
    public partial class NullableCaloriesPerServing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CaloriesPerServing",
                table: "Recipes",
                type: "float",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CaloriesPerServing",
                table: "Recipes",
                type: "float",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "float",
                oldNullable: true);
        }
    }
}
