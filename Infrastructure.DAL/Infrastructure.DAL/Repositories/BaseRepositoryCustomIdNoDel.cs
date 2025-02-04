using Infrastructure.DAL.DTO;
using Infrastructure.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.Repositories;

public interface IBaseRepositoryCustomIdNoDel<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<TEntity?> GetById(TId id);
    public DbSearchResponse<TEntity> Search(DbSearchRequest<TEntity> dbSearchRequest);
    public DbSearchResponse<TEntity> Search<TOrderKey>(DbSearchRequest<TEntity, TOrderKey> dbSearchRequest);
    public Task<TEntity> Create(TEntity entity);
    public Task<TEntity> CreateWithoutSave(TEntity entity);
    public Task Update(TEntity updatedEntity);

    public Task SaveChanges();
}

public abstract class BaseRepositoryCustomIdNoDel<TEntity, TId> : IBaseRepositoryCustomIdNoDel<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    protected abstract IQueryable<TEntity> DefaultQuery { get; }
    protected abstract DbSet<TEntity> Set { get; }

    private readonly DbContext dbContext;

    protected BaseRepositoryCustomIdNoDel(DbContext dbContext)
    {
        this.dbContext = dbContext;
        dbContext.Set<TEntity>();
    }

    public Task<TEntity?> GetById(TId id)
    {
        return DefaultQuery.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public DbSearchResponse<TEntity> Search<TOrderKey>(DbSearchRequest<TEntity, TOrderKey> dbSearchRequest)
    {
        var query = DefaultQuery.Where(dbSearchRequest.WhereExpr);

        return new DbSearchResponse<TEntity>(
            query.Count(),
            query
                .ApplyOrdering(dbSearchRequest.DbOrderByRequest)
                .Skip(dbSearchRequest.Skip)
                .Take(dbSearchRequest.Take)
                .ToArray());
    }

    public DbSearchResponse<TEntity> Search(DbSearchRequest<TEntity> dbSearchRequest)
    {
        var query = DefaultQuery.Where(dbSearchRequest.WhereExpr);

        return new DbSearchResponse<TEntity>(
            query.Count(),
            query
                .Skip(dbSearchRequest.Skip)
                .Take(dbSearchRequest.Take)
                .ToArray());
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await Set.AddAsync(entity);
        await SaveChanges();
        return entity;
    }

    public async Task<TEntity> CreateWithoutSave(TEntity entity)
    {
        await Set.AddAsync(entity);
        return entity;
    }

    public async Task Update(TEntity updatedEntity)
    {
        await Set
            .Where(e => e.Id.Equals(updatedEntity.Id))
            .ExecuteUpdateAsync(propCalls => propCalls
                .SetProperty(e => e, updatedEntity));
        await dbContext.SaveChangesAsync();
    }


    public Task SaveChanges() => dbContext.SaveChangesAsync();
}