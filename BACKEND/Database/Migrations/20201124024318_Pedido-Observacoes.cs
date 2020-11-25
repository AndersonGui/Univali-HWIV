using Microsoft.EntityFrameworkCore.Migrations;

namespace HW.Migrations
{
    public partial class PedidoObservacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "ListaProduto");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Pedido",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "$2a$11$zZZ7aZKvgyMw0.7tTcakw.SY7Mw4OnVyNLTbMy89yGyiTLN28OrHG");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Pedido");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "ListaProduto",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "Senha",
                value: "$2a$11$EV6Je1kQce2xPJTKjG06o.mDFMWlaGWyFaRRZRn.IYE2wimJDzspO");
        }
    }
}
