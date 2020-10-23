using Microsoft.EntityFrameworkCore.Migrations;

namespace Plan.Infrastructure.Migrations
{
    public partial class Enum2String : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TrybStudiow",
                table: "Grupa",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TrybStudiow",
                table: "Grupa",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
