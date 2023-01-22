using MediatR;

using WaitForIt.Application.Contracts;
using WaitForIt.Domain;

namespace WaitForIt.Application.Features.Commands.Remove;

public class RemoveEntityHandler : IRequestHandler<RemoveEntity, (bool IsDelete, string Msg)>
{
    private readonly IAsyncEventRepository EventRepository;

    public RemoveEntityHandler(IAyncEntityRepository<Entity> entityRepository, IAsyncEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }
    public async Task<(bool IsDelete, string Msg)> Handle(RemoveEntity RemoveEntityRequest, CancellationToken cancellationToken)
    {
        return await EventRepository.AppendEventToEntityAsync(RemoveEntityRequest);

    }
}
