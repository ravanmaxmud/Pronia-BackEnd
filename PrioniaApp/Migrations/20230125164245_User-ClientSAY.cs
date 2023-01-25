using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrioniaApp.Migrations
{
    /// <inheritdoc />
    public partial class UserClientSAY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientSays_Roles_RoleId",
                table: "ClientSays");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "ClientSays",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientSays_RoleId",
                table: "ClientSays",
                newName: "IX_ClientSays_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSays_Users_UserId",
                table: "ClientSays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientSays_Users_UserId",
                table: "ClientSays");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ClientSays",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientSays_UserId",
                table: "ClientSays",
                newName: "IX_ClientSays_RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSays_Roles_RoleId",
                table: "ClientSays",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
