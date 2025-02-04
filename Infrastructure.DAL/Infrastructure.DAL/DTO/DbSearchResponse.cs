namespace Infrastructure.DAL.DTO;

public record class DbSearchResponse<TEntity>(
    int TotalCount,
    TEntity[] Items)
{
}