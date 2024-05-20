using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUTUKA.Migrations
{
    /// <inheritdoc />
    public partial class Migrations5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteModeloId_Cliente",
                table: "Lance",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lance_ClienteModeloId_Cliente",
                table: "Lance",
                column: "ClienteModeloId_Cliente");

            migrationBuilder.AddForeignKey(
                name: "FK_Lance_Cliente_ClienteModeloId_Cliente",
                table: "Lance",
                column: "ClienteModeloId_Cliente",
                principalTable: "Cliente",
                principalColumn: "Id_Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lance_Cliente_ClienteModeloId_Cliente",
                table: "Lance");

            migrationBuilder.DropIndex(
                name: "IX_Lance_ClienteModeloId_Cliente",
                table: "Lance");

            migrationBuilder.DropColumn(
                name: "ClienteModeloId_Cliente",
                table: "Lance");
        }
    }
}
