using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Palindromes.API.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "catalog_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Palindrome",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 1000, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palindrome", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palindrome");

            migrationBuilder.DropSequence(
                name: "catalog_hilo");
        }
    }
}
