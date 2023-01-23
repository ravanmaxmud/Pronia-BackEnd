using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrioniaApp.Migrations
{
    /// <inheritdoc />
    public partial class ProductCatagoryies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catagoryies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagoryies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catagoryies_Catagoryies_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Catagoryies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCatagoryies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CatagoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCatagoryies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCatagoryies_Catagoryies_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "Catagoryies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCatagoryies_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catagoryies_ParentId",
                table: "Catagoryies",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatagoryies_CatagoryId",
                table: "ProductCatagoryies",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCatagoryies_ProductId",
                table: "ProductCatagoryies",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCatagoryies");

            migrationBuilder.DropTable(
                name: "Catagoryies");
        }
    }
}
