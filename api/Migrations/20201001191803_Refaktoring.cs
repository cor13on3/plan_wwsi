using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Test2.Migrations
{
    public partial class Refaktoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Semestr",
                table: "Zjazd");

            migrationBuilder.DropColumn(
                name: "CzasDo",
                table: "Lekcja");

            migrationBuilder.DropColumn(
                name: "CzasOd",
                table: "Lekcja");

            migrationBuilder.DropColumn(
                name: "CzyOdpracowanie",
                table: "Lekcja");

            migrationBuilder.DropColumn(
                name: "Stopien",
                table: "Grupa");

            migrationBuilder.DropColumn(
                name: "Tryb",
                table: "Grupa");

            migrationBuilder.AddColumn<int>(
                name: "RodzajSemestru",
                table: "Zjazd",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "IdPrzedmiotu",
                table: "Przedmiot",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "CzyOdpracowanie",
                table: "LekcjaGrupa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "IdPrzedmiotu",
                table: "Lekcja",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<string>(
                name: "GodzinaDo",
                table: "Lekcja",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GodzinaOd",
                table: "Lekcja",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NrZjazdu",
                table: "GrupaZjazd",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "Semestr",
                table: "Grupa",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                name: "StopienStudiow",
                table: "Grupa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrybStudiow",
                table: "Grupa",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RodzajSemestru",
                table: "Zjazd");

            migrationBuilder.DropColumn(
                name: "CzyOdpracowanie",
                table: "LekcjaGrupa");

            migrationBuilder.DropColumn(
                name: "GodzinaDo",
                table: "Lekcja");

            migrationBuilder.DropColumn(
                name: "GodzinaOd",
                table: "Lekcja");

            migrationBuilder.DropColumn(
                name: "StopienStudiow",
                table: "Grupa");

            migrationBuilder.DropColumn(
                name: "TrybStudiow",
                table: "Grupa");

            migrationBuilder.AddColumn<int>(
                name: "Semestr",
                table: "Zjazd",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<short>(
                name: "IdPrzedmiotu",
                table: "Przedmiot",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<short>(
                name: "IdPrzedmiotu",
                table: "Lekcja",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CzasDo",
                table: "Lekcja",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CzasOd",
                table: "Lekcja",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "CzyOdpracowanie",
                table: "Lekcja",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "NrZjazdu",
                table: "GrupaZjazd",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<short>(
                name: "Semestr",
                table: "Grupa",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Stopien",
                table: "Grupa",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tryb",
                table: "Grupa",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
