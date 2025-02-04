using Infrastructure.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public interface IBaseRepository<TEntity> : IBaseRepositoryCustomId<TEntity, Guid>
    where TEntity : class, IEntity<Guid>
{
}

public abstract class BaseRepository<TEntity> : BaseRepositoryCustomId<TEntity, Guid>, IBaseRepository<TEntity>
    where TEntity : class, IEntity<Guid>
{
    protected BaseRepository(DbContext dbContext) : base(dbContext)
    {
    }
}