using CQRS.Core.Handlers;
using Hello.Common.Commands;
using Werter.HelloCmd.Domain.Aggregates;

namespace Werter.HelloCmd.Api.Commands;

public class CommandHandler : ICommandHandler
{
    private readonly IEventSourcingHandler<HelloAggregate> _eventSourcingHandler;


    public CommandHandler(IEventSourcingHandler<HelloAggregate> eventSourcingHandler)
    {
        _eventSourcingHandler = eventSourcingHandler;
    }

    public async Task HandlerAsync(SendHelloCommand command)
    {
        var aggregate = new HelloAggregate(command.Id, command.Message);
        await _eventSourcingHandler.SaveAsync(aggregate);
    }
}