namespace Werter.HelloCmd.Api.Commands;

public interface ICommandHandler
{
    Task HandlerAsync(SendHelloCommand command);
}