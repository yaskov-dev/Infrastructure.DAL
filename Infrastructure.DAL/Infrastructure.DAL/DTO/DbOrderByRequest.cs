#nullable enable

using System.Linq.Expressions;

namespace Infrastructure.DAL.DTO;

public record class DbOrderByRequest<TEntity, TKey>(
    Expression<Func<TEntity,TKey>> KeySelector,
    OrderByDirection Direction = OrderByDirection.Asc)
{
}

public enum OrderByDirection
{
    Asc,
    Desc,
}

public static class QueryableOrderByRequestExtensions
{
    public static IOrderedQueryable<TEntity> ApplyOrdering<TEntity, TKey>(this IQueryable<TEntity> query, DbOrderByRequest<TEntity, TKey> dbOrderByRequest)
    {
        return dbOrderByRequest.Direction == OrderByDirection.Asc
            ? query.OrderBy(dbOrderByRequest.KeySelector)
            : query.OrderByDescending(dbOrderByRequest.KeySelector);
    }
}