using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SB.Financa.DAL.Migrations
{
    public partial class vInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaBancaria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Agencia = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Conta = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Digito = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Ativa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaBancaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaCartao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apelido = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(16)", nullable: true),
                    Bandeira = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCartao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etiqueta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etiqueta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(10)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planejamento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Meta = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    DataInicial = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataFinal = table.Column<DateTime>(type: "datetime", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(30)", nullable: false),
                    EtiquetaId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planejamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planejamento_Etiqueta_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cadastro = table.Column<DateTime>(nullable: false),
                    Vencimento = table.Column<DateTime>(nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    EtiquetaId = table.Column<int>(nullable: false),
                    PessoaId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    ValorPago = table.Column<decimal>(nullable: false),
                    ContaCartaoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimento_ContaCartao_ContaCartaoId",
                        column: x => x.ContaCartaoId,
                        principalTable: "ContaCartao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimento_Etiqueta_EtiquetaId",
                        column: x => x.EtiquetaId,
                        principalTable: "Etiqueta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimento_Pessoa_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentoBaixa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataBaixa = table.Column<DateTime>(nullable: false),
                    DataEvento = table.Column<DateTime>(nullable: false),
                    Direcao = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    ValorBaixa = table.Column<decimal>(nullable: false),
                    MovimentoId = table.Column<int>(nullable: false),
                    ContaBancariaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoBaixa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoBaixa_ContaBancaria_ContaBancariaId",
                        column: x => x.ContaBancariaId,
                        principalTable: "ContaBancaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentoBaixa_Movimento_MovimentoId",
                        column: x => x.MovimentoId,
                        principalTable: "Movimento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_ContaCartaoId",
                table: "Movimento",
                column: "ContaCartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_EtiquetaId",
                table: "Movimento",
                column: "EtiquetaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimento_PessoaId",
                table: "Movimento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBaixa_ContaBancariaId",
                table: "MovimentoBaixa",
                column: "ContaBancariaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoBaixa_MovimentoId",
                table: "MovimentoBaixa",
                column: "MovimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Planejamento_EtiquetaId",
                table: "Planejamento",
                column: "EtiquetaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentoBaixa");

            migrationBuilder.DropTable(
                name: "Planejamento");

            migrationBuilder.DropTable(
                name: "ContaBancaria");

            migrationBuilder.DropTable(
                name: "Movimento");

            migrationBuilder.DropTable(
                name: "ContaCartao");

            migrationBuilder.DropTable(
                name: "Etiqueta");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
