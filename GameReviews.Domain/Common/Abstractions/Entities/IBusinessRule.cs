namespace GameReviews.Domain.Common.Abstractions.Entities;
public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}