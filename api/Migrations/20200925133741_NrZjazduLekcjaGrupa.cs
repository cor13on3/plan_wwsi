using Microsoft.EntityFrameworkCore.Migrations;

namespace Test2.Migrations
{
    public partial class NrZjazduLekcjaGrupa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NrZjazdu",
                table: "LekcjaGrupa",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NrZjazdu",
                table: "LekcjaGrupa");
        }
    }
}
