using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class DzienTyg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DzienTygodnia",
                table: "LekcjaGrupa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CzyZdalnie",
                table: "GrupaZjazd",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DzienTygodnia",
                table: "LekcjaGrupa");

            migrationBuilder.DropColumn(
                name: "CzyZdalnie",
                table: "GrupaZjazd");
        }
    }
}
