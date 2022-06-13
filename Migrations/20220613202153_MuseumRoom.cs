using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace museum_management.Migrations
{
    public partial class MuseumRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MuseumRoom",
                table: "Artwork",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MuseumRoom",
                table: "Artwork");
        }
    }
}
