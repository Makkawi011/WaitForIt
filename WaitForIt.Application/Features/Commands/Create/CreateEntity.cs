using MediatR;

using WaitForIt.Domain;

namespace WaitForIt.Application.Features.Commands.Create;

public class CreateEntity : Event , IRequest<Guid>
{
    public required string Name { get; init; }
    public string? Description { get; set; }
    public required int Quantity { get; init; }

}
