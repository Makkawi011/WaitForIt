using MediatR;

using WaitForIt.Application.Contracts;
using WaitForIt.Domain;

namespace WaitForIt.Application.Features.Commands.Create;

public class CreateEntityHandler : IRequestHandler<CreateEntity, Guid>
{
    private readonly IAsyncEventRepository EventRepository;

    public CreateEntityHandler(IAsyncEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }

    public async Task<Guid> Handle(CreateEntity CreateEntityRequest, CancellationToken cancellationToken)
    {

        var id = await EventRepository.AppendEventToEntityAsync(CreateEntityRequest);

        return new Guid(id);
    }
}
