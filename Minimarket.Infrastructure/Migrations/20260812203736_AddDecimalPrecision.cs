using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minimarket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7711));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7717));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7721));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7723));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7726));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7771), new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7772) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 37, 32, 817, DateTimeKind.Utc).AddTicks(7395));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(2943));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(2947));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(2949));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(2952));

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(2955));

            migrationBuilder.UpdateData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaRegistro" },
                values: new object[] { new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(3005), new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(3005) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 8, 12, 20, 21, 44, 772, DateTimeKind.Utc).AddTicks(2585));
        }
    }
}
