using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrioniaApp.Migrations
{
    /// <inheritdoc />
    public partial class ClientSayContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ClientSays",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ClientSays");
        }
    }
}
