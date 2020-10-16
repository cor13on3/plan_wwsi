using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Plan.Infrastructure.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupa",
                columns: table => new
                {
                    NrGrupy = table.Column<string>(nullable: false),
                    Semestr = table.Column<int>(nullable: false),
                    TrybStudiow = table.Column<int>(nullable: false),
                    StopienStudiow = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupa", x => x.NrGrupy);
                });

            migrationBuilder.CreateTable(
                name: "Przedmiot",
                columns: table => new
                {
                    IdPrzedmiotu = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przedmiot", x => x.IdPrzedmiotu);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    IdSali = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(nullable: true),
                    Rodzaj = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.IdSali);
                });

            migrationBuilder.CreateTable(
                name: "Specjalnosc",
                columns: table => new
                {
                    IdSpecjalnosci = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nazwa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specjalnosc", x => x.IdSpecjalnosci);
                });

            migrationBuilder.CreateTable(
                name: "Wykladowca",
                columns: table => new
                {
                    IdWykladowcy = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tytul = table.Column<string>(nullable: true),
                    Imie = table.Column<string>(nullable: true),
                    Nazwisko = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wykladowca", x => x.IdWykladowcy);
                });

            migrationBuilder.CreateTable(
                name: "Zjazd",
                columns: table => new
                {
                    IdZjazdu = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataOd = table.Column<DateTime>(nullable: false),
                    DataDo = table.Column<DateTime>(nullable: false),
                    RodzajSemestru = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zjazd", x => x.IdZjazdu);
                });

            migrationBuilder.CreateTable(
                name: "Lekcja",
                columns: table => new
                {
                    IdLekcji = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GodzinaOd = table.Column<string>(nullable: true),
                    GodzinaDo = table.Column<string>(nullable: true),
                    Forma = table.Column<int>(nullable: false),
                    IdPrzedmiotu = table.Column<int>(nullable: false),
                    IdSali = table.Column<int>(nullable: false),
                    IdWykladowcy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekcja", x => x.IdLekcji);
                    table.ForeignKey(
                        name: "FK_Lekcja_Przedmiot_IdPrzedmiotu",
                        column: x => x.IdPrzedmiotu,
                        principalTable: "Przedmiot",
                        principalColumn: "IdPrzedmiotu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lekcja_Sala_IdSali",
                        column: x => x.IdSali,
                        principalTable: "Sala",
                        principalColumn: "IdSali",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lekcja_Wykladowca_IdWykladowcy",
                        column: x => x.IdWykladowcy,
                        principalTable: "Wykladowca",
                        principalColumn: "IdWykladowcy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WyklSpec",
                columns: table => new
                {
                    IdWykladowcy = table.Column<int>(nullable: false),
                    IdSpecjalnosci = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyklSpec", x => new { x.IdWykladowcy, x.IdSpecjalnosci });
                    table.ForeignKey(
                        name: "FK_WyklSpec_Specjalnosc_IdSpecjalnosci",
                        column: x => x.IdSpecjalnosci,
                        principalTable: "Specjalnosc",
                        principalColumn: "IdSpecjalnosci",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WyklSpec_Wykladowca_IdWykladowcy",
                        column: x => x.IdWykladowcy,
                        principalTable: "Wykladowca",
                        principalColumn: "IdWykladowcy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupaZjazd",
                columns: table => new
                {
                    IdZjazdu = table.Column<int>(nullable: false),
                    NrGrupy = table.Column<string>(nullable: false),
                    NrZjazdu = table.Column<int>(nullable: false),
                    CzyOdpracowanie = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupaZjazd", x => new { x.IdZjazdu, x.NrGrupy });
                    table.ForeignKey(
                        name: "FK_GrupaZjazd_Zjazd_IdZjazdu",
                        column: x => x.IdZjazdu,
                        principalTable: "Zjazd",
                        principalColumn: "IdZjazdu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupaZjazd_Grupa_NrGrupy",
                        column: x => x.NrGrupy,
                        principalTable: "Grupa",
                        principalColumn: "NrGrupy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LekcjaGrupa",
                columns: table => new
                {
                    IdLekcji = table.Column<int>(nullable: false),
                    NrGrupy = table.Column<string>(nullable: false),
                    NrZjazdu = table.Column<int>(nullable: false),
                    DzienTygodnia = table.Column<int>(nullable: false),
                    CzyOdpracowanie = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LekcjaGrupa", x => new { x.IdLekcji, x.NrGrupy });
                    table.ForeignKey(
                        name: "FK_LekcjaGrupa_Lekcja_IdLekcji",
                        column: x => x.IdLekcji,
                        principalTable: "Lekcja",
                        principalColumn: "IdLekcji",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LekcjaGrupa_Grupa_NrGrupy",
                        column: x => x.NrGrupy,
                        principalTable: "Grupa",
                        principalColumn: "NrGrupy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupaZjazd_NrGrupy",
                table: "GrupaZjazd",
                column: "NrGrupy");

            migrationBuilder.CreateIndex(
                name: "IX_Lekcja_IdPrzedmiotu",
                table: "Lekcja",
                column: "IdPrzedmiotu");

            migrationBuilder.CreateIndex(
                name: "IX_Lekcja_IdSali",
                table: "Lekcja",
                column: "IdSali");

            migrationBuilder.CreateIndex(
                name: "IX_Lekcja_IdWykladowcy",
                table: "Lekcja",
                column: "IdWykladowcy");

            migrationBuilder.CreateIndex(
                name: "IX_LekcjaGrupa_NrGrupy",
                table: "LekcjaGrupa",
                column: "NrGrupy");

            migrationBuilder.CreateIndex(
                name: "IX_WyklSpec_IdSpecjalnosci",
                table: "WyklSpec",
                column: "IdSpecjalnosci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupaZjazd");

            migrationBuilder.DropTable(
                name: "LekcjaGrupa");

            migrationBuilder.DropTable(
                name: "WyklSpec");

            migrationBuilder.DropTable(
                name: "Zjazd");

            migrationBuilder.DropTable(
                name: "Lekcja");

            migrationBuilder.DropTable(
                name: "Grupa");

            migrationBuilder.DropTable(
                name: "Specjalnosc");

            migrationBuilder.DropTable(
                name: "Przedmiot");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Wykladowca");
        }
    }
}
