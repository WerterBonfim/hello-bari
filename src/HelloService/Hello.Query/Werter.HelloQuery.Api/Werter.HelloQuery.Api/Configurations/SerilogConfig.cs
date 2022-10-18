using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.SystemConsole.Themes;
using EventHandler = Werter.HelloQuery.Infrastructure.Handlers.EventHandler;

namespace Werter.HelloQuery.Api.Configurations;

public static class SerilogConfig
{
    public static void AddSerilog(this IServiceCollection services, IHostBuilder host )
    {
        var isEventHandler = Matching.FromSource("Werter.HelloQuery.Infrastructure.Handlers.EventHandler");
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Filter.ByExcluding(x => !isEventHandler(x))
            .WriteTo.Async(wt => 
                wt.Console(
                    theme: AnsiConsoleTheme.Sixteen,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
            .CreateLogger();

        host.UseSerilog(Log.Logger);
    }
}