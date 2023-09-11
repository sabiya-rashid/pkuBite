using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pkuBite.Migrations
{
    /// <inheritdoc />
    public partial class imageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Foods",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Foods");
        }
    }
}
