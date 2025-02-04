using Infrastructure.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public interface IBaseRepositoryNoDel<TEntity> : IBaseRepositoryCustomIdNoDel<TEntity, Guid>
    where TEntity : class, IEntity<Guid>
{
}

public abstract class BaseRepositoryNoDel<TEntity> : BaseRepositoryCustomIdNoDel<TEntity, Guid>, IBaseRepositoryNoDel<TEntity>
    where TEntity : class, IEntity<Guid>
{
    public BaseRepositoryNoDel(DbContext dbContext) : base(dbContext)
    {
    }
}