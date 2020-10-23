using Microsoft.EntityFrameworkCore.Migrations;

namespace Plan.Infrastructure.Migrations
{
    public partial class Enum2String2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RodzajSemestru",
                table: "Zjazd",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Rodzaj",
                table: "Sala",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Forma",
                table: "Lekcja",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "StopienStudiow",
                table: "Grupa",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RodzajSemestru",
                table: "Zjazd",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Rodzaj",
                table: "Sala",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Forma",
                table: "Lekcja",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "StopienStudiow",
                table: "Grupa",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
