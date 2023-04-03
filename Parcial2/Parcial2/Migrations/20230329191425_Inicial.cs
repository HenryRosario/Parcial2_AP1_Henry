using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parcial2.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produccion",
                columns: table => new
                {
                    ProduccionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produccion", x => x.ProduccionId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Costo = table.Column<decimal>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    Existencia = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "ProduccionDetalle",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduccionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduccionDetalle", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_ProduccionDetalle_Produccion_ProduccionId",
                        column: x => x.ProduccionId,
                        principalTable: "Produccion",
                        principalColumn: "ProduccionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductoId", "Costo", "Descripcion", "Existencia", "Precio" },
                values: new object[,]
                {
                    { 2, 10m, "nuez", 15, 100m },
                    { 3, 10m, "almendra", 100, 100m },
                    { 4, 10m, "chia", 10, 100m },
                    { 5, 10m, "Mani", 20, 100m },
                    { 6, 10m, "pasas", 10, 100m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduccionDetalle_ProduccionId",
                table: "ProduccionDetalle",
                column: "ProduccionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduccionDetalle");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Produccion");
        }
    }
}
