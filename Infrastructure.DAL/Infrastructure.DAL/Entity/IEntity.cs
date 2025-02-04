namespace Infrastructure.DAL.Entity;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}