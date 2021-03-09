using Microsoft.EntityFrameworkCore.Migrations;

namespace Plan.Infrastructure.Migrations
{
    public partial class Mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lekcja_Sala_IdSali",
                table: "Lekcja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd");

            migrationBuilder.AlterColumn<int>(
                name: "IdSali",
                table: "Lekcja",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd",
                columns: new[] { "IdZjazdu", "NrGrupy", "NrZjazdu", "CzyOdpracowanie" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lekcja_Sala_IdSali",
                table: "Lekcja",
                column: "IdSali",
                principalTable: "Sala",
                principalColumn: "IdSali",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lekcja_Sala_IdSali",
                table: "Lekcja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd");

            migrationBuilder.AlterColumn<int>(
                name: "IdSali",
                table: "Lekcja",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupaZjazd",
                table: "GrupaZjazd",
                columns: new[] { "IdZjazdu", "NrGrupy" });

            migrationBuilder.AddForeignKey(
                name: "FK_Lekcja_Sala_IdSali",
                table: "Lekcja",
                column: "IdSali",
                principalTable: "Sala",
                principalColumn: "IdSali",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
