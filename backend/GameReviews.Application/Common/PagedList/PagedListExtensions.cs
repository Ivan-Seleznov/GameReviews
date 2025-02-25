namespace GameReviews.Application.Common.PagedList;

public static class PagedListExtensions
{
    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int page, int pageSize, int? totalCount = null)
    {
        var list = source.ToList();
        return PagedList<T>.Create(list.ToList(), page, pageSize, totalCount ?? list.Count());
    }
}