using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEI.Migrations
{
    public partial class FixRecipeIngredientTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "amount",
                table: "RecipeIngredient",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "RecipeIngredient",
                newName: "amount");
        }
    }
}
