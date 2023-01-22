namespace WaitForIt.Domain;

public sealed class Entity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Quantity { get; set; }

    public List<Event> events { get; set; }


}