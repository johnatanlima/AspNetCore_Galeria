using Microsoft.EntityFrameworkCore.Migrations;

namespace Galeria.Migrations
{
    public partial class Mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FotoTopo",
                table: "Albuns",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FotoTopo",
                table: "Albuns",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
