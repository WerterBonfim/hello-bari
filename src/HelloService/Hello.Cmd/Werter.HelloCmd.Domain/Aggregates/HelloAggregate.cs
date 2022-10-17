using CQRS.Core.Domain;
using Hello.Common.Events;

namespace Werter.HelloCmd.Domain.Aggregates;

public class HelloAggregate : AggregateRoot
{
    
    
    public HelloAggregate()
    {
        
    }

    public HelloAggregate(Guid id, string message)
    {
        RaiseEvent(new HelloSendEvent
        {
            Id = id,
            Message = message,
            DatePosted = DateTime.Now
        });
    }

    public void Apply(HelloSendEvent sendEvent)
    {
        _id = sendEvent.Id;
        
    }
}