using MediatR;

using WaitForIt.Application.Contracts;
using WaitForIt.Domain;

namespace WaitForIt.Application.Features.Queries.GetAllEventsQuery;

public class GetAllEventsQuery : IRequest<IEnumerable<GetAllEventsQueryViewModel>>
{
    public required Guid Id { get; set; }
}

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<GetAllEventsQueryViewModel>>
{
    public IAsyncEventRepository EventRepository { get; set; }
    public GetAllEventsQueryHandler(IAsyncEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }


    public async Task<IEnumerable<GetAllEventsQueryViewModel>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await EventRepository.GetEventsByEntityIdAsync(request.Id);
       
        return events
            .Select(e =>
                new GetAllEventsQueryViewModel
                {
                    EventTime = e.EventTime,
                    EventName = e.EventName,
                    Entity = e.Entity
                });
    }

}

public class GetAllEventsQueryViewModel
{
    public string EventName { get; set; }
    public TimeOnly EventTime { get; set; }
    public Entity? Entity { get; set; }
}