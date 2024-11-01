namespace GameReviews.Domain.Common.Abstractions.Entities;
public interface IBusinessRule
{
    string Message { get; }
    Task<bool> IsBrokenAsync();
}