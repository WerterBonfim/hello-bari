using Hello.Common.Events;

namespace Werter.HelloQuery.Infrastructure.Handlers;

public class EventHandler : IEventHandler
{
    
    
    public Task On(HelloSendEvent @event)
    {

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Mensagem consumida. Conteudo: {@event.Message} DataHoraPulbicação: {@event.DatePosted}");
        
        return Task.CompletedTask;
    }
}