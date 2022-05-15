using BestMovies.Api.Models.Filters;

namespace BestMovies.Api.Data.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, FilterBase filter)
    {
        return query.Skip(filter.Offset).Take(filter.Limit);
    }
}
