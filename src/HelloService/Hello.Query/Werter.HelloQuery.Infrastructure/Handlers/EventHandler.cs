using Hello.Common.Events;
using Microsoft.Extensions.Logging;

namespace Werter.HelloQuery.Infrastructure.Handlers;

public class EventHandler : IEventHandler
{
    private readonly ILogger<EventHandler> _logger;

    public EventHandler(ILogger<EventHandler> logger)
    {
        _logger = logger;
    }

    public Task On(HelloSendEvent @event)
    {
        _logger.LogInformation(@event.ToString());
        return Task.CompletedTask;
    }
}