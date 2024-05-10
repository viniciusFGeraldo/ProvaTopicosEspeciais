using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThiagoSchwantesDeMoura.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoFolha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "valor",
                table: "Folhas",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "Folhas",
                newName: "Quantidade");

            migrationBuilder.RenameColumn(
                name: "mes",
                table: "Folhas",
                newName: "Mes");

            migrationBuilder.RenameColumn(
                name: "ano",
                table: "Folhas",
                newName: "Ano");

            migrationBuilder.AddColumn<double>(
                name: "ImpostoFgts",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ImpostoInss",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ImpostoIrrf",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioBruto",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalarioLiquido",
                table: "Folhas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImpostoFgts",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "ImpostoInss",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "ImpostoIrrf",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "SalarioBruto",
                table: "Folhas");

            migrationBuilder.DropColumn(
                name: "SalarioLiquido",
                table: "Folhas");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Folhas",
                newName: "valor");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Folhas",
                newName: "quantidade");

            migrationBuilder.RenameColumn(
                name: "Mes",
                table: "Folhas",
                newName: "mes");

            migrationBuilder.RenameColumn(
                name: "Ano",
                table: "Folhas",
                newName: "ano");
        }
    }
}
