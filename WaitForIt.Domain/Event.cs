using System.ComponentModel.DataAnnotations.Schema;

namespace WaitForIt.Domain;

public class Event
{
    public Guid EventId;
    public string EventName => GetType().Name;
    public TimeOnly EventTime => TimeOnly.FromDateTime(DateTime.UtcNow);

    public Entity? Entity;
    [ForeignKey(nameof(EntityId))]
    public required string EntityId;
}