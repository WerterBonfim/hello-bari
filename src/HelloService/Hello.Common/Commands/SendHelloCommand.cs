using CQRS.Core.Messages;

namespace Hello.Common.Commands;

public class SendHelloCommand : BaseCommand
{
    public string Message { get; set; }
}