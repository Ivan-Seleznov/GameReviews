using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameReviews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameEntityUserEntity_Games_GamesId",
                table: "GameEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_GameEntityUserEntity_Users_UsersId",
                table: "GameEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokenEntity_Users_UserId",
                table: "RefreshTokenEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permissions_PermissionId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Roles_RoleId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUserEntity_Roles_RolesId",
                table: "RoleUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleUserEntity_Users_UsersId",
                table: "RoleUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleUserEntity",
                table: "RoleUserEntity");

            migrationBuilder.DropIndex(
                name: "IX_RoleUserEntity_UsersId",
                table: "RoleUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewEntity",
                table: "ReviewEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokenEntity",
                table: "RefreshTokenEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEntityUserEntity",
                table: "GameEntityUserEntity");

            migrationBuilder.DropIndex(
                name: "IX_GameEntityUserEntity_GamesId",
                table: "GameEntityUserEntity");

            migrationBuilder.RenameTable(
                name: "RoleUserEntity",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "ReviewEntity",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "RefreshTokenEntity",
                newName: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "GameEntityUserEntity",
                newName: "UserGame");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "RolePermission",
                newName: "PermissionsId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "RolePermission",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                newName: "IX_RolePermission_PermissionsId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewEntity_AuthorId_GameId",
                table: "Reviews",
                newName: "UX_Review_User_Game");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokenEntity_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "UsersId", "RolesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGame",
                table: "UserGame",
                columns: new[] { "GamesId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RolesId",
                table: "UserRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_GameId",
                table: "Reviews",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_UsersId",
                table: "UserGame",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Games_GameId",
                table: "Reviews",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_AuthorId",
                table: "Reviews",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permissions_PermissionsId",
                table: "RolePermission",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Roles_RolesId",
                table: "RolePermission",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Games_GamesId",
                table: "UserGame",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGame_Users_UsersId",
                table: "UserGame",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Roles_RolesId",
                table: "UserRole",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_UsersId",
                table: "UserRole",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Games_GameId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_AuthorId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Permissions_PermissionsId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermission_Roles_RolesId",
                table: "RolePermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Games_GamesId",
                table: "UserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGame_Users_UsersId",
                table: "UserGame");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Roles_RolesId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_UsersId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_RolesId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGame",
                table: "UserGame");

            migrationBuilder.DropIndex(
                name: "IX_UserGame_UsersId",
                table: "UserGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Review_GameId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "RoleUserEntity");

            migrationBuilder.RenameTable(
                name: "UserGame",
                newName: "GameEntityUserEntity");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "ReviewEntity");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshTokenEntity");

            migrationBuilder.RenameColumn(
                name: "PermissionsId",
                table: "RolePermission",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "RolePermission",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_RolePermission_PermissionsId",
                table: "RolePermission",
                newName: "IX_RolePermission_PermissionId");

            migrationBuilder.RenameIndex(
                name: "UX_Review_User_Game",
                table: "ReviewEntity",
                newName: "IX_ReviewEntity_AuthorId_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokenEntity",
                newName: "IX_RefreshTokenEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleUserEntity",
                table: "RoleUserEntity",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEntityUserEntity",
                table: "GameEntityUserEntity",
                columns: new[] { "UsersId", "GamesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewEntity",
                table: "ReviewEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokenEntity",
                table: "RefreshTokenEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserEntity_UsersId",
                table: "RoleUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_GameEntityUserEntity_GamesId",
                table: "GameEntityUserEntity",
                column: "GamesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntityUserEntity_Games_GamesId",
                table: "GameEntityUserEntity",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameEntityUserEntity_Users_UsersId",
                table: "GameEntityUserEntity",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokenEntity_Users_UserId",
                table: "RefreshTokenEntity",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permissions_PermissionId",
                table: "RolePermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Roles_RoleId",
                table: "RolePermission",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUserEntity_Roles_RolesId",
                table: "RoleUserEntity",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleUserEntity_Users_UsersId",
                table: "RoleUserEntity",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
