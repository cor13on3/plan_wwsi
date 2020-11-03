using Microsoft.EntityFrameworkCore.Migrations;

namespace Plan.Infrastructure.Migrations
{
    public partial class LekcjaGrupaKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LekcjaGrupa",
                table: "LekcjaGrupa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LekcjaGrupa",
                table: "LekcjaGrupa",
                columns: new[] { "IdLekcji", "NrGrupy", "NrZjazdu", "DzienTygodnia" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LekcjaGrupa",
                table: "LekcjaGrupa");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LekcjaGrupa",
                table: "LekcjaGrupa",
                columns: new[] { "IdLekcji", "NrGrupy" });
        }
    }
}
