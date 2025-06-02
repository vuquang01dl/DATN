using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TourStatusLogs_TourId",
                table: "TourStatusLogs",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourStatusLogs_Tours_TourId",
                table: "TourStatusLogs",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourStatusLogs_Tours_TourId",
                table: "TourStatusLogs");

            migrationBuilder.DropIndex(
                name: "IX_TourStatusLogs_TourId",
                table: "TourStatusLogs");
        }
    }
}
