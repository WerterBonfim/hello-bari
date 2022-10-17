using Confluent.Kafka;
using CQRS.Core.Consumers;
using Werter.HelloQuery.Infrastructure.Consumers;
using Werter.HelloQuery.Infrastructure.Handlers;

namespace Werter.HelloQuery.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEventHandler, Infrastructure.Handlers.EventHandler>();
        services.Configure<ConsumerConfig>(configuration.GetSection(nameof(ConsumerConfig)));
        services.AddScoped<IEventConsumer, EventConsumer>();
        
        
        services.AddHostedService<ConsumerHostedService>();
    }
}