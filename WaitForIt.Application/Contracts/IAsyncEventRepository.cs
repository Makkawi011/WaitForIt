using WaitForIt.Domain;

namespace WaitForIt.Application.Contracts;

public interface IAsyncEventRepository
{
    Task<dynamic> AppendEventToEntityAsync(Event @event);
    Task<IEnumerable<Event>> GetEventsByEntityIdAsync(Guid EntId);
}
