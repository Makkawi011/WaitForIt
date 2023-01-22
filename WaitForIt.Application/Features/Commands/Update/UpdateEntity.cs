using MediatR;

using WaitForIt.Domain;

namespace WaitForIt.Application.Features.Commands.Update;

public class UpdateEntity : Event, IRequest<(bool IsUpdate, string Msg)>
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Quantity { get; set; }

}
