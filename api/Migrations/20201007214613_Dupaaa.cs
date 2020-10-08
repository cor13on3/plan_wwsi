using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class Dupaaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DzienOdpracowywany",
                table: "Zjazd");

            migrationBuilder.DropColumn(
                name: "Forma",
                table: "Przedmiot");

            migrationBuilder.AddColumn<int>(
                name: "Forma",
                table: "Lekcja",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Forma",
                table: "Lekcja");

            migrationBuilder.AddColumn<DateTime>(
                name: "DzienOdpracowywany",
                table: "Zjazd",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Forma",
                table: "Przedmiot",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
