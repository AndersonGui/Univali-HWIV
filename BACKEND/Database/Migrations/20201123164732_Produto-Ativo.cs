using Microsoft.EntityFrameworkCore.Migrations;

namespace HW.Migrations
{
    public partial class ProdutoAtivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Produto",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 1,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 2,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: 3,
                column: "Ativo",
                value: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "$2a$11$EV6Je1kQce2xPJTKjG06o.mDFMWlaGWyFaRRZRn.IYE2wimJDzspO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Produto");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "$2a$11$2..p09DMd3N5ouH1uKBTz.i3sgT2GQtibnXB5ARD7ZUKJdt6BwsGi");
        }
    }
}
