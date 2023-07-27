namespace Application.Common.Persistance;

public interface IRepository<TAggregate> where TAggregate : class
{
    Task<TAggregate?> GetByIdAsync(object id);

    void Insert(TAggregate entity);

    Task<bool> RemoveAsync(object id);

    void Update(TAggregate entity);
}