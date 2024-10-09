using Igdb.Abstractions.Models;

namespace GameReviews.Infrastructure.Constants;
internal static class TableNames
{
    internal const string Roles = nameof(Roles);
    internal const string Permissions = nameof(Permissions);
    internal const string Users = nameof(Users);
    internal const string Games = nameof(Games);
    internal const string Reviews = "ReviewEntity";
    internal const string UsersGames = "GameEntityUserEntity";
}