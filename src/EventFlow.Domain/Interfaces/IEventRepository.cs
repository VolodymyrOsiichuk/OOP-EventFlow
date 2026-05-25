using EventFlow.Domain.Entities;

namespace EventFlow.Domain.Interfaces;

public interface IEventRepository : IRepository<Event>
{
}