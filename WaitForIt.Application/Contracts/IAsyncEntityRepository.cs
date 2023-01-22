using WaitForIt.Domain;

namespace WaitForIt.Application.Contracts;

public interface IAsyncEntityRepository 
    : IAyncEntityRepository<Entity>
{
    Task<dynamic> ApplyEventAsync(Event @event);
}
