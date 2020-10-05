using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Test2.Migrations
{
    public partial class IdRename : Migration
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

            migrationBuilder.DropColumn(
                name: "IdWykladowcy",
                table: "Wykladowca");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Wykladowca",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wykladowca",
                table: "Wykladowca",
                column: "Id");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Wykladowca");

            migrationBuilder.AddColumn<int>(
                name: "IdWykladowcy",
                table: "Wykladowca",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wykladowca",
                table: "Wykladowca",
                column: "IdWykladowcy");

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
    }
}
