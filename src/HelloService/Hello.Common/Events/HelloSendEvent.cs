using CQRS.Core.Events;

namespace Hello.Common.Events;

public class HelloSendEvent : BaseEvent
{
    public HelloSendEvent() : base(nameof(HelloSendEvent))
    {
    }

    public string Message { get; set; }
    public DateTime DatePosted { get; set; }

    public override string ToString() =>
        $"Id: {Id} Mensagem: {Message}. TipoEvento: {Type} DataHora: {DatePosted}";
    
}