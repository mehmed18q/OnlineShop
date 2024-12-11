using Core.Models;

namespace Core.Utilities
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> If<T>(this IQueryable<T> sourceQuery, bool condition, Func<IQueryable<T>, IQueryable<T>> query)
        {
            return condition ? query(sourceQuery) : sourceQuery;
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, Pagination pager)
        {
            return queryable.Skip(pager.SkipSize).Take(pager.PageSize);
        }
    }

}
