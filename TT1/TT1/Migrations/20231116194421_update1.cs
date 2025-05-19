using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TT1.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Accounts_accountIdId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "accountIdId",
                table: "RefreshTokens",
                newName: "accountId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_accountIdId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_accountId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Accounts_accountId",
                table: "RefreshTokens",
                column: "accountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Accounts_accountId",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "accountId",
                table: "RefreshTokens",
                newName: "accountIdId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_accountId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_accountIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Accounts_accountIdId",
                table: "RefreshTokens",
                column: "accountIdId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
