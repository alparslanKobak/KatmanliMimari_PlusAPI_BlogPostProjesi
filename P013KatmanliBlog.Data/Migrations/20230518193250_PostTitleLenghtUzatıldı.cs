using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P013KatmanliBlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostTitleLenghtUzatıldı : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 5, 18, 22, 32, 50, 894, DateTimeKind.Local).AddTicks(7876), new Guid("09f8331d-8b9a-479c-9cd6-4d860515ea9d") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreateDate", "UserGuid" },
                values: new object[] { new DateTime(2023, 5, 17, 19, 56, 14, 108, DateTimeKind.Local).AddTicks(8560), new Guid("9c869f16-ee72-496e-b6df-4e00ea30ce3e") });
        }
    }
}
