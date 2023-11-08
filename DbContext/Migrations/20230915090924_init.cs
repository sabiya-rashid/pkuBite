using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbContext.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourites_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Plant foods" },
                    { 2, "Savoury foods" },
                    { 3, "Diary foods" },
                    { 4, "Drinks" },
                    { 5, "Snack foods" },
                    { 6, "Baking foods" },
                    { 7, "Sweets, Spreads" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Nuts and seeds" },
                    { 2, 1, "Dried Fruits" },
                    { 3, 2, "Meat, Fish, Eggs" },
                    { 4, 2, "Vegeterian" },
                    { 5, 3, "Regular cheese" },
                    { 6, 3, "Milk products" },
                    { 7, 4, "Tea and coffee" },
                    { 8, 4, "Fizzy drinks and Squash juice" },
                    { 9, 5, "Sweet snacks" },
                    { 10, 5, "Salty snacks" },
                    { 11, 6, "Cakes" },
                    { 12, 6, "Biscuits" },
                    { 13, 7, "Sugars and syrups" },
                    { 14, 7, "Confectionary" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, "Fresh almonds", "imageurl", "Almonds", 5.9900000000000002, 1 },
                    { 2, "Roasted cashews", "imageurl", "Cashews", 6.4900000000000002, 1 },
                    { 3, "Sun-dried raisins", "imageurl", "Raisins", 3.9900000000000002, 2 },
                    { 4, "Dried apricots", "imageurl", "Apricots", 7.9900000000000002, 2 },
                    { 5, "Fresh chicken", "imageurl", "Chicken", 8.9900000000000002, 3 },
                    { 6, "Wild-caught salmon", "imageurl", "Salmon", 12.99, 3 },
                    { 7, "Organic tofu", "imageurl", "Tofu", 4.4900000000000002, 4 },
                    { 8, "Fresh eggplant", "imageurl", "Eggplant", 2.9900000000000002, 4 },
                    { 9, "Sharp cheddar cheese", "imageurl", "Cheddar Cheese", 4.9900000000000002, 5 },
                    { 10, "Fresh mozzarella cheese", "imageurl", "Mozzarella Cheese", 5.4900000000000002, 5 },
                    { 11, "Whole milk", "imageurl", "Milk", 2.29, 6 },
                    { 12, "Greek yogurt", "imageurl", "Yogurt", 3.4900000000000002, 6 },
                    { 13, "Premium black tea", "imageurl", "Black Tea", 3.9900000000000002, 7 },
                    { 14, "Arabica coffee", "imageurl", "Coffee", 6.9900000000000002, 7 },
                    { 15, "Carbonated soda", "imageurl", "Soda", 1.99, 8 },
                    { 16, "Assorted fruit juices", "imageurl", "Fruit Juice", 4.4900000000000002, 8 },
                    { 17, "Dark chocolate bar", "imageurl", "Chocolate", 2.9900000000000002, 9 },
                    { 18, "Assorted candies", "imageurl", "Candy", 1.49, 9 },
                    { 19, "Classic potato chips", "imageurl", "Potato Chips", 2.79, 10 },
                    { 20, "Crunchy pretzels", "imageurl", "Pretzels", 2.29, 10 },
                    { 21, "Decadent chocolate cake", "imageurl", "Chocolate Cake", 8.9900000000000002, 11 },
                    { 22, "Homemade carrot cake", "imageurl", "Carrot Cake", 7.4900000000000002, 11 },
                    { 23, "Freshly baked cookies", "imageurl", "Chocolate Chip Cookies", 3.9900000000000002, 12 },
                    { 24, "Healthy oatmeal cookies", "imageurl", "Oatmeal Cookies", 3.4900000000000002, 12 },
                    { 25, "Granulated sugar", "imageurl", "Sugar", 2.4900000000000002, 13 },
                    { 26, "Pure maple syrup", "imageurl", "Maple Syrup", 5.9900000000000002, 13 },
                    { 27, "Assorted gummy bears", "imageurl", "Gummy Bears", 1.99, 14 },
                    { 28, "Black licorice twists", "imageurl", "Licorice", 2.29, 14 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_FoodId",
                table: "Favourites",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_SubCategoryId",
                table: "Foods",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
