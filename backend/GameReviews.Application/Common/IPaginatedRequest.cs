namespace GameReviews.Application.Common;

public interface IPaginatedRequest
{
     int Page {get; set;}
     int PageSize { get; set; }
     int MaxPageSize => 50;
}