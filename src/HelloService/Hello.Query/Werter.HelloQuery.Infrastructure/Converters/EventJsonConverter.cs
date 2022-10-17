using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;
using Hello.Common.Events;

namespace Werter.HelloQuery.Infrastructure.Converters;


public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
            throw new JsonException($"Falhou em converter {nameof(JsonDocument)}");

        if (!doc.RootElement.TryGetProperty("Type", out var type))
            throw new JsonException($"Não pode detectar o tipo da propriedade discriminatoria");

        var typeDescriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        return typeDescriminator switch
        {
            nameof(HelloSendEvent) => JsonSerializer.Deserialize<HelloSendEvent>(json, options),
            _ => throw new JsonException($"{typeDescriminator} não é suportado!!")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}