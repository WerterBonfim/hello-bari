using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using Hello.Common.Commands;
using Werter.HelloCmd.Api.Commands;
using Werter.HelloCmd.Domain.Aggregates;
using Werter.HelloCmd.Infrastructure.Config;
using Werter.HelloCmd.Infrastructure.Dispatchers;
using Werter.HelloCmd.Infrastructure.Handlers;
using Werter.HelloCmd.Infrastructure.Producers;
using Werter.HelloCmd.Infrastructure.Repositories;
using Werter.HelloCmd.Infrastructure.Stores;

namespace Werter.HelloCmd.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbConfig>(configuration.GetSection(nameof(MongoDbConfig)));
        services.Configure<ProducerConfig>(configuration.GetSection(nameof(ProducerConfig)));


        services.AddScoped<IEventStoreRepository, EventStoreRepository>();
        services.AddScoped<IEventProducer, EventProducer>();
        services.AddScoped<IEventStore, EventStore>();
        services.AddScoped<IEventSourcingHandler<HelloAggregate>, EventSourcingHandler>();
        services.AddScoped<ICommandHandler, CommandHandler>();


        var commandHandler = services
            .BuildServiceProvider()
            .GetRequiredService<ICommandHandler>();


        var dispatcher = new CommandDispatcher();
        dispatcher.RegisterHandler<SendHelloCommand>(commandHandler.HandlerAsync);

        services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

        services.AddHostedService<HelloWorldHostedService>();
    }
}