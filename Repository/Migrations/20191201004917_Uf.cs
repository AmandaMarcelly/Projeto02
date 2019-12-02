using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Uf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especialidade",
                table: "Medicos");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "Medicos",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uf",
                table: "Medicos");

            migrationBuilder.AddColumn<int>(
                name: "Especialidade",
                table: "Medicos",
                nullable: false,
                defaultValue: 0);
        }
    }
}
