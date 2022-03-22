using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Azure_api.Migrations
{
    public partial class jorge1222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aniversario",
                table: "Amigos");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Amigos");

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    AmigosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Aniversario = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.AmigosId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.AddColumn<DateTime>(
                name: "Aniversario",
                table: "Amigos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Amigos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
