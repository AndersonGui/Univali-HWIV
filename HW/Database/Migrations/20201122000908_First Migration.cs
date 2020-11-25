using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HW.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(type: "varchar(100)", nullable: false),
                    Identificador = table.Column<string>(type: "varchar(100)", nullable: false),
                    ValorFinal = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(type: "varchar(100)", nullable: false),
                    Senha = table.Column<string>(type: "varchar(400)", nullable: false),
                    Perfil = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListaProduto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdPedido = table.Column<int>(nullable: false),
                    IdProduto = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Observacoes = table.Column<string>(type: "varchar(200)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaProduto_Pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListaProduto_Produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "Nome", "Valor" },
                values: new object[,]
                {
                    { 1, "Produto 01", 2.99m },
                    { 2, "Produto 02", 5.99m },
                    { 3, "Produto 03", 19.24m }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Login", "Perfil", "Senha" },
                values: new object[] { 1, "admin", "Administrador", "$2a$11$2..p09DMd3N5ouH1uKBTz.i3sgT2GQtibnXB5ARD7ZUKJdt6BwsGi" });

            migrationBuilder.CreateIndex(
                name: "IX_ListaProduto_IdPedido",
                table: "ListaProduto",
                column: "IdPedido");

            migrationBuilder.CreateIndex(
                name: "IX_ListaProduto_IdProduto",
                table: "ListaProduto",
                column: "IdProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaProduto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
