using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReviewConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewEntity_AuthorId",
                table: "ReviewEntity");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "ReviewEntity",
                columns: new[] { "AuthorId", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "ReviewEntity");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewEntity_AuthorId",
                table: "ReviewEntity",
                column: "AuthorId");
        }
    }
}
