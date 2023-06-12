using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P013KatmanliBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class KontrolAmacliDeneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 5, 25, 23, 4, 57, 1, DateTimeKind.Local).AddTicks(178), new Guid("0a651b1a-b0a1-4a46-aff5-08259ca692d2") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 5, 18, 22, 32, 50, 894, DateTimeKind.Local).AddTicks(7876), new Guid("09f8331d-8b9a-479c-9cd6-4d860515ea9d") });
        }
    }
}
