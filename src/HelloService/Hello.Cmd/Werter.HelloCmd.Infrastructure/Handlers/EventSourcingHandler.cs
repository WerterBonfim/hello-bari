using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using Werter.HelloCmd.Domain.Aggregates;

namespace Werter.HelloCmd.Infrastructure.Handlers;

public class EventSourcingHandler : IEventSourcingHandler<HelloAggregate>
{
    private readonly IEventStore _eventStore;

    public EventSourcingHandler(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }
    
    public async Task SaveAsync(AggregateRoot aggregate)
    {
        await _eventStore.SaveEventAsync(
            aggregate.Id, 
            aggregate.GetUncommittedChanges(), 
            aggregate.Version);
        aggregate.MarkChangesAsCommitted();
    }

    public async Task<HelloAggregate> GetByIdAsync(Guid aggregateId)
    {
        var aggregate = new HelloAggregate();
        var events = await _eventStore.GetEventsAsync(aggregateId);

        if (events == null || !events.Any()) return aggregate;
        
        aggregate.ReplayEvents(events);
        aggregate.Version = events.Select(x => x.Version).Max();

        return aggregate;

    }
}