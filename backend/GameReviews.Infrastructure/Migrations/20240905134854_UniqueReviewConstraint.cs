using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueReviewConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "ReviewEntity");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "ReviewEntity",
                columns: new[] { "AuthorId", "GameId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "ReviewEntity");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "ReviewEntity",
                columns: new[] { "AuthorId", "GameId" });
        }
    }
}
