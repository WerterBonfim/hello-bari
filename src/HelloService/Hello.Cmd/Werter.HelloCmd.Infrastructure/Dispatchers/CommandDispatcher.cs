using CQRS.Core.Infrastructure;
using CQRS.Core.Messages;

namespace Werter.HelloCmd.Infrastructure.Dispatchers;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new();

    public void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
    {
        if (_handlers.ContainsKey(typeof(T)))
            throw new IndexOutOfRangeException("Você não pode registrar o mesmo command handler duas vezes!");

        _handlers.Add(typeof(T), x => handler((T)x));
    }

    public Task SendAsync(BaseCommand command)
    {
        if (!_handlers.TryGetValue(command.GetType(), out var handler))
            throw new ArgumentNullException(nameof(handler), "O command handler não foi regitrado!");

        handler(command);

        return Task.CompletedTask;
    }
}