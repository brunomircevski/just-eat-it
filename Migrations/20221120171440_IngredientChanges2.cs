using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEI.Migrations
{
    public partial class IngredientChanges2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sugars",
                table: "Ingredients",
                newName: "Salt");

            migrationBuilder.AddColumn<float>(
                name: "Fiber",
                table: "Ingredients",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fiber",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Ingredients",
                newName: "Sugars");
        }
    }
}
