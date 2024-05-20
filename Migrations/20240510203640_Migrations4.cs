using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUTUKA.Migrations
{
    /// <inheritdoc />
    public partial class Migrations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Viaturas",
                table: "Viaturas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participações",
                table: "Participações");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leilões",
                table: "Leilões");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lances",
                table: "Lances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionários",
                table: "Funcionários");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Viaturas",
                newName: "Viatura");

            migrationBuilder.RenameTable(
                name: "Participações",
                newName: "Participacao");

            migrationBuilder.RenameTable(
                name: "Leilões",
                newName: "Leilao");

            migrationBuilder.RenameTable(
                name: "Lances",
                newName: "Lance");

            migrationBuilder.RenameTable(
                name: "Funcionários",
                newName: "Funcionario");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viatura",
                table: "Viatura",
                column: "Id_Viatura");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participacao",
                table: "Participacao",
                column: "Id_Participacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leilao",
                table: "Leilao",
                column: "Id_Leilao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lance",
                table: "Lance",
                column: "Id_Lance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario",
                column: "Id_Funcionario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id_Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Viatura",
                table: "Viatura");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participacao",
                table: "Participacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Leilao",
                table: "Leilao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lance",
                table: "Lance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionario",
                table: "Funcionario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Viatura",
                newName: "Viaturas");

            migrationBuilder.RenameTable(
                name: "Participacao",
                newName: "Participações");

            migrationBuilder.RenameTable(
                name: "Leilao",
                newName: "Leilões");

            migrationBuilder.RenameTable(
                name: "Lance",
                newName: "Lances");

            migrationBuilder.RenameTable(
                name: "Funcionario",
                newName: "Funcionários");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viaturas",
                table: "Viaturas",
                column: "Id_Viatura");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participações",
                table: "Participações",
                column: "Id_Participacao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Leilões",
                table: "Leilões",
                column: "Id_Leilao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lances",
                table: "Lances",
                column: "Id_Lance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionários",
                table: "Funcionários",
                column: "Id_Funcionario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id_Cliente");
        }
    }
}
