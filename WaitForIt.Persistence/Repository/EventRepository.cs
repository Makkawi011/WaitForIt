using WaitForIt.Application.Contracts;
using WaitForIt.Domain;
using Dapper;

namespace WaitForIt.Persistence.Repository;

public class EventRepository : DBRepository, IAsyncEventRepository 
{
    public IAsyncEntityRepository entityRepository { get; set; }

    public EventRepository(IAsyncEntityRepository entityRepository)
    {
        this.entityRepository = entityRepository;
    }

    public async Task<dynamic> AppendEventToEntityAsync(Event @event)
    {
        using (var transaction = db.BeginTransaction())
        {

            var result = await entityRepository.ApplyEventAsync(@event);


            string sql = "INSERT INTO Events (EventName, EventTime, EntitiesId) VALUES (@EventName, @EventTime, @EntitiesId)";
            
            var param =
                new
                {
                    EventName = @event.EventName,
                    EventTime = @event.EventTime,
                    EntitiesId = @event.EntityId
                };

            await db.QueryAsync<Guid>(sql, param);

            return result;
        };
        
    }

    public async Task<IEnumerable<Event>> GetEventsByEntityIdAsync(Guid EntId)
    {

        string sql = "SELECT * FROM Events JOIN Entities ON Events.EntitiesId = Entities.Id WHERE Entities.Id = @Id";
        var events = await db.QueryAsync<Event>(sql, new { Id = EntId });
        return events;
        

    }
}