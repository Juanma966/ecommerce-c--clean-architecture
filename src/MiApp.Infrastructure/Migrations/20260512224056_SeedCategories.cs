using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "Prendas",
                newName: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000001"), "Electrónica" },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000002"), "Ropa" },
                    { new Guid("a1b2c3d4-0000-0000-0000-000000000003"), "Hogar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prendas_CategoryId",
                table: "Prendas",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prendas_Categories_CategoryId",
                table: "Prendas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prendas_Categories_CategoryId",
                table: "Prendas");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Prendas_CategoryId",
                table: "Prendas");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Prendas",
                newName: "Categoria");
        }
    }
}
