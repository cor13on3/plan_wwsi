using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Test2.Migrations
{
    public partial class Dupa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lekcja_Wykladowca_IdWykladowcy",
                table: "Lekcja");

            migrationBuilder.DropForeignKey(
                name: "FK_WyklSpec_Wykladowca_IdWykladowcy",
                table: "WyklSpec");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wykladowca",
                table: "Wykladowca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd");

            migrationBuilder.DropIndex(
                name: "IX_GrupaZjazd_IdZjazdu",
                table: "GrupaZjazd");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wykladowca");

            migrationBuilder.AddColumn<DateTime>(
                name: "DzienOdpracowywany",
                table: "Zjazd",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdWykladowcy",
                table: "Wykladowca",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<bool>(
                name: "CzyOdpracowanie",
                table: "GrupaZjazd",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wykladowca",
                table: "Wykladowca",
                column: "IdWykladowcy");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd",
                columns: new[] { "IdZjazdu", "NrGrupy" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lekcja_Wykladowca_IdWykladowcy",
                table: "Lekcja",
                column: "IdWykladowcy",
                principalTable: "Wykladowca",
                principalColumn: "IdWykladowcy",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WyklSpec_Wykladowca_IdWykladowcy",
                table: "WyklSpec",
                column: "IdWykladowcy",
                principalTable: "Wykladowca",
                principalColumn: "IdWykladowcy",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lekcja_Wykladowca_IdWykladowcy",
                table: "Lekcja");

            migrationBuilder.DropForeignKey(
                name: "FK_WyklSpec_Wykladowca_IdWykladowcy",
                table: "WyklSpec");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wykladowca",
                table: "Wykladowca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd");

            migrationBuilder.DropColumn(
                name: "DzienOdpracowywany",
                table: "Zjazd");

            migrationBuilder.DropColumn(
                name: "IdWykladowcy",
                table: "Wykladowca");

            migrationBuilder.DropColumn(
                name: "CzyOdpracowanie",
                table: "GrupaZjazd");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wykladowca",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wykladowca",
                table: "Wykladowca",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd",
                columns: new[] { "NrZjazdu", "IdZjazdu", "NrGrupy" });

            migrationBuilder.CreateIndex(
                name: "IX_GrupaZjazd_IdZjazdu",
                table: "GrupaZjazd",
                column: "IdZjazdu");

            migrationBuilder.AddForeignKey(
                name: "FK_Lekcja_Wykladowca_IdWykladowcy",
                table: "Lekcja",
                column: "IdWykladowcy",
                principalTable: "Wykladowca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WyklSpec_Wykladowca_IdWykladowcy",
                table: "WyklSpec",
                column: "IdWykladowcy",
                principalTable: "Wykladowca",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
