namespace GameReviews.Infrastructure.Constants.IGDB;

public static class IGDBFieldConstants
{
    public const string GameInfo = "id,name,summary,cover.*";
    public const string GameDetails = "id,name,summary,first_release_date,game_status.status,game_type.type,category,status,cover.*, screenshots.*";
}