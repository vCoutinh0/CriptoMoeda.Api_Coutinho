using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbRepositoryAdapter.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoricoNegociacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaiorPreco = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    MenorPreco = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    QuantidadeNegociada = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    MaiorPrecoOfertado = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    MenorPrecoOfertado = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoNegociacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NegociacoesDoDia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sigla = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaiorPreco = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    MenorPreco = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    QuantidadeNegociada = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    MaiorPrecoOfertado = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    MenorPrecoOfertado = table.Column<decimal>(type: "decimal(14,8)", precision: 14, scale: 8, nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegociacoesDoDia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoNegociacoes_Sigla",
                table: "HistoricoNegociacoes",
                column: "Sigla");

            migrationBuilder.CreateIndex(
                name: "IX_NegociacoesDoDia_Sigla",
                table: "NegociacoesDoDia",
                column: "Sigla",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoNegociacoes");

            migrationBuilder.DropTable(
                name: "NegociacoesDoDia");
        }
    }
}
