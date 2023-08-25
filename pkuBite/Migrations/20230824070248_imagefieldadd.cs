using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pkuBite.Migrations
{
    /// <inheritdoc />
    public partial class imagefieldadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Foods");
        }
    }
}
