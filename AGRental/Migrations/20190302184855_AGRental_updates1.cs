using Microsoft.EntityFrameworkCore.Migrations;

namespace AGRental.Migrations
{
    public partial class AGRental_updates1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bathrooms",
                table: "Properties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "bedrooms",
                table: "Properties",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bathrooms",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "bedrooms",
                table: "Properties");
        }
    }
}
