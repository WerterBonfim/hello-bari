using CQRS.Core.Infrastructure;
using Hello.Common.Commands;
using Microsoft.Extensions.Hosting;

namespace Werter.HelloCmd.Infrastructure.Producers;

public class HelloWorldHostedService : IHostedService, IDisposable
{
    private readonly ICommandDispatcher _commandDispatcher;
    private Timer _timer;

    public HelloWorldHostedService(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return Task.CompletedTask;
        
        _timer = new Timer(SendNewHelloMessage, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        
        return Task.CompletedTask;
    }

    private async void SendNewHelloMessage(object state)
    {
        await _commandDispatcher.SendAsync(new SendHelloCommand
        {
            Id = Guid.NewGuid(),
            Message = "Hello World"
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}