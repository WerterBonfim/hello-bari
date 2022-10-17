using System.Text.Json;
using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Events;
using Microsoft.Extensions.Options;
using Werter.HelloQuery.Infrastructure.Converters;
using Werter.HelloQuery.Infrastructure.Handlers;

namespace Werter.HelloQuery.Infrastructure.Consumers;


public class EventConsumer : IEventConsumer
{
    private readonly ConsumerConfig _config;
    private readonly IEventHandler _eventHandler;

    public EventConsumer(IOptions<ConsumerConfig> config, IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;
        _config = config.Value;
    }

    public void Consume(string topic)
    {
        using var consumer = new ConsumerBuilder<string, string>(_config)
            .SetKeyDeserializer(Deserializers.Utf8)
            .SetValueDeserializer(Deserializers.Utf8)
            .Build();

        consumer.Subscribe(topic);

        while (true)
        {
            var consumeResult = consumer.Consume();
            if (consumeResult?.Message == null) continue;

            var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
            var @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, options);

            var handlerMethod = _eventHandler.GetType()
                .GetMethod("On", new[] { @event.GetType() });

            if (handlerMethod == null)
                throw new ArgumentNullException(nameof(handlerMethod), "Não encontrou o metodo event handler ");

            handlerMethod.Invoke(_eventHandler, new object[] { @event });
            consumer.Commit(consumeResult);
        }
    }
}