namespace Kwetterprise.EventSourcing.Client
{
    using System;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Kwetterprise.EventSourcing.Client.Models.Event;

    internal sealed class KwetterpriseEventConverter : JsonConverter<EventBase>
    {
        public override EventBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var json = reader.GetString();

            var type = EventBase.EventTypeEnumToType(
                (EventType)JsonDocument.Parse(json).RootElement
                    .EnumerateObject()
                    .First(x => x.NameEquals(nameof(EventBase.Type)))
                    .Value.GetInt32());

            return (EventBase)JsonSerializer.Deserialize(json, type);
        }

        public override void Write(Utf8JsonWriter writer, EventBase value, JsonSerializerOptions options)
        {
            throw new InvalidOperationException();
        }
    }
}