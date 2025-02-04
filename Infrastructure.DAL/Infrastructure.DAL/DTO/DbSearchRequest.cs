using System.Linq.Expressions;

namespace Infrastructure.DAL.DTO;

public record class DbSearchRequest<TEntity>(
    Expression<Func<TEntity, bool>> WhereExpr,
    int Take = 100,
    int Skip = 0)
{
}

public record class DbSearchRequest<TEntity, TKey>(
    Expression<Func<TEntity, bool>> WhereExpr,
    DbOrderByRequest<TEntity, TKey> DbOrderByRequest,
    int Take = 100,
    int Skip = 0)
{
}