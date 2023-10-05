using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCiudadEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cliente_ciudad_CiudadesId",
                table: "cliente");

            migrationBuilder.DropIndex(
                name: "IX_cliente_CiudadesId",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "CiudadesId",
                table: "cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CiudadesId",
                table: "cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cliente_CiudadesId",
                table: "cliente",
                column: "CiudadesId");

            migrationBuilder.AddForeignKey(
                name: "FK_cliente_ciudad_CiudadesId",
                table: "cliente",
                column: "CiudadesId",
                principalTable: "ciudad",
                principalColumn: "Id");
        }
    }
}
