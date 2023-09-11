using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pkuBite.Migrations
{
    /// <inheritdoc />
    public partial class imageUrsl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Foods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Foods",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
