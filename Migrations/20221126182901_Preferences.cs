using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JEI.Migrations
{
    public partial class Preferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Recipes",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "Preferable",
                table: "Categories",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CategoryUser",
                columns: table => new
                {
                    PreferedCategoriesId = table.Column<int>(type: "int", nullable: false),
                    PreferingUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryUser", x => new { x.PreferedCategoriesId, x.PreferingUsersId });
                    table.ForeignKey(
                        name: "FK_CategoryUser_Categories_PreferedCategoriesId",
                        column: x => x.PreferedCategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryUser_Users_PreferingUsersId",
                        column: x => x.PreferingUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryUser_PreferingUsersId",
                table: "CategoryUser",
                column: "PreferingUsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryUser");

            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Preferable",
                table: "Categories");
        }
    }
}
