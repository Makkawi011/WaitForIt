using MediatR;

using WaitForIt.Application.Contracts;

namespace WaitForIt.Application.Features.Commands.Update;
public class UpdateEntityHandler : IRequestHandler<UpdateEntity, (bool IsUpdate, string Msg)>
{
    public IAsyncEventRepository eventRepository { get; set; }
    public UpdateEntityHandler(IAsyncEventRepository eventRepository)
    {
        this.eventRepository = eventRepository;
    }

    public async Task<(bool IsUpdate, string Msg)> Handle(UpdateEntity UpdateEntityRequest, CancellationToken cancellationToken)
    {
        return await eventRepository.AppendEventToEntityAsync(UpdateEntityRequest);
    }
}
