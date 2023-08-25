using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pkuBite.Migrations
{
    /// <inheritdoc />
    public partial class fruitmodelChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_SubCategories_SubcategoryId",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "Foods",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_SubcategoryId",
                table: "Foods",
                newName: "IX_Foods_SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_SubCategories_SubCategoryId",
                table: "Foods",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_SubCategories_SubCategoryId",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Foods",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_SubCategoryId",
                table: "Foods",
                newName: "IX_Foods_SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_SubCategories_SubcategoryId",
                table: "Foods",
                column: "SubcategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
