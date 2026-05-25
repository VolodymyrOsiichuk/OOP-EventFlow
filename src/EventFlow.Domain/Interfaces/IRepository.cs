namespace EventFlow.Domain.Interfaces;

public interface IRepository<T>
{
    void Add(T entity);

    T? GetById(Guid id);

    IEnumerable<T> GetAll();
}