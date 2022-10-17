using Hello.Common.Events;

namespace Werter.HelloQuery.Infrastructure.Handlers;

public interface IEventHandler
{
    Task On(HelloSendEvent @event);
}