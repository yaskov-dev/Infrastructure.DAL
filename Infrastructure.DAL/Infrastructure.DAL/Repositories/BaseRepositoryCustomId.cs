using Infrastructure.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public interface IBaseRepositoryCustomId<TEntity, TId> : IBaseRepositoryCustomIdNoDel<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task Delete(TId id);
}

public abstract class BaseRepositoryCustomId<TEntity, TId> : BaseRepositoryCustomIdNoDel<TEntity, TId>, IBaseRepositoryCustomId<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    protected BaseRepositoryCustomId(DbContext dbContext) : base(dbContext)
    {
    }

    public Task Delete(TId id)
    {
         return Set
            .Where(e => e.Id.Equals(id))
            .ExecuteDeleteAsync();
    }
}