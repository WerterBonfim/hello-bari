using CQRS.Core.Messages;

namespace Werter.HelloCmd.Api.Commands;

public class SendHelloCommand : BaseCommand
{
    public string Message { get; set; }
}