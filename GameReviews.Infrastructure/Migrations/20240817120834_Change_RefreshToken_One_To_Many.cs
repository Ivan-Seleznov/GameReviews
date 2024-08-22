using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Change_RefreshToken_One_To_Many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokenEntity_UserId",
                table: "RefreshTokenEntity");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenEntity_UserId",
                table: "RefreshTokenEntity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokenEntity_UserId",
                table: "RefreshTokenEntity");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokenEntity_UserId",
                table: "RefreshTokenEntity",
                column: "UserId",
                unique: true);
        }
    }
}
