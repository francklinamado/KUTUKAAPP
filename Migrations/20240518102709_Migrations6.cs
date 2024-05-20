using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KUTUKA.Migrations
{
    /// <inheritdoc />
    public partial class Migrations6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Id_Lance",
                table: "Participacao",
                newName: "Id_Viatura");

            migrationBuilder.AddColumn<int>(
                name: "Id_Leilao",
                table: "Participacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Leilao",
                keyColumn: "Estado",
                keyValue: null,
                column: "Estado",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Leilao",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Inicio",
                table: "Leilao",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Fim",
                table: "Leilao",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participacao_Id_Cliente",
                table: "Participacao",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Participacao_Id_Leilao",
                table: "Participacao",
                column: "Id_Leilao");

            migrationBuilder.CreateIndex(
                name: "IX_Participacao_Id_Viatura",
                table: "Participacao",
                column: "Id_Viatura");

            migrationBuilder.CreateIndex(
                name: "IX_Leilao_Id_Funcionario",
                table: "Leilao",
                column: "Id_Funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_Lance_Id_Cliente",
                table: "Lance",
                column: "Id_Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Lance_Id_Participacao",
                table: "Lance",
                column: "Id_Participacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Lance_Cliente_Id_Cliente",
                table: "Lance",
                column: "Id_Cliente",
                principalTable: "Cliente",
                principalColumn: "Id_Cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lance_Participacao_Id_Participacao",
                table: "Lance",
                column: "Id_Participacao",
                principalTable: "Participacao",
                principalColumn: "Id_Participacao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leilao_Funcionario_Id_Funcionario",
                table: "Leilao",
                column: "Id_Funcionario",
                principalTable: "Funcionario",
                principalColumn: "Id_Funcionario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participacao_Cliente_Id_Cliente",
                table: "Participacao",
                column: "Id_Cliente",
                principalTable: "Cliente",
                principalColumn: "Id_Cliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participacao_Leilao_Id_Leilao",
                table: "Participacao",
                column: "Id_Leilao",
                principalTable: "Leilao",
                principalColumn: "Id_Leilao",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Participacao_Viatura_Id_Viatura",
                table: "Participacao",
                column: "Id_Viatura",
                principalTable: "Viatura",
                principalColumn: "Id_Viatura",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lance_Cliente_Id_Cliente",
                table: "Lance");

            migrationBuilder.DropForeignKey(
                name: "FK_Lance_Participacao_Id_Participacao",
                table: "Lance");

            migrationBuilder.DropForeignKey(
                name: "FK_Leilao_Funcionario_Id_Funcionario",
                table: "Leilao");

            migrationBuilder.DropForeignKey(
                name: "FK_Participacao_Cliente_Id_Cliente",
                table: "Participacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Participacao_Leilao_Id_Leilao",
                table: "Participacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Participacao_Viatura_Id_Viatura",
                table: "Participacao");

            migrationBuilder.DropIndex(
                name: "IX_Participacao_Id_Cliente",
                table: "Participacao");

            migrationBuilder.DropIndex(
                name: "IX_Participacao_Id_Leilao",
                table: "Participacao");

            migrationBuilder.DropIndex(
                name: "IX_Participacao_Id_Viatura",
                table: "Participacao");

            migrationBuilder.DropIndex(
                name: "IX_Leilao_Id_Funcionario",
                table: "Leilao");

            migrationBuilder.DropIndex(
                name: "IX_Lance_Id_Cliente",
                table: "Lance");

            migrationBuilder.DropIndex(
                name: "IX_Lance_Id_Participacao",
                table: "Lance");

            migrationBuilder.DropColumn(
                name: "Id_Leilao",
                table: "Participacao");

            migrationBuilder.RenameColumn(
                name: "Id_Viatura",
                table: "Participacao",
                newName: "Id_Lance");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Leilao",
                type: "varchar(8)",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8)",
                oldMaxLength: 8)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Inicio",
                table: "Leilao",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data_Fim",
                table: "Leilao",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

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
    }
}
