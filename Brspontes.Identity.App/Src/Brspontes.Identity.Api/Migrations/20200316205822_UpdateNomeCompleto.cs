using Microsoft.EntityFrameworkCore.Migrations;

namespace Brspontes.Identity.Api.Migrations
{
    public partial class UpdateNomeCompleto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeComplet",
                table: "AspNetUsers",
                newName: "NomeCompleto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeCompleto",
                table: "AspNetUsers",
                newName: "NomeComplet");
        }
    }
}
