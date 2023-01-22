using MediatR;

using WaitForIt.Domain;

namespace WaitForIt.Application.Features.Commands.Remove;

public class RemoveEntity : Event, IRequest<(bool IsDelete, string Msg)>
{
    public required Guid Id { get; init; }
}