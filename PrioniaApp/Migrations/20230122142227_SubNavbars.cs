﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrioniaApp.Migrations
{
    /// <inheritdoc />
    public partial class SubNavbars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubNavbars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NavbarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubNavbars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubNavbars_Navbars_NavbarId",
                        column: x => x.NavbarId,
                        principalTable: "Navbars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubNavbars_NavbarId",
                table: "SubNavbars",
                column: "NavbarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubNavbars");
        }
    }
}
