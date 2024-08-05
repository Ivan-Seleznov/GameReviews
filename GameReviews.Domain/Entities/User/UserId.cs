namespace GameReviews.Domain.Entities.User;

public class UserId
{
    public int Value { get; set; }

    public UserId(int value = default)
    {
        Value = value;
    }
}

