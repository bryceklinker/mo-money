using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Simulator.Models.Publishing
{
    public class MarketEventModel
    {
        public MetadataModel Metadata { get; }
        public JObject Payload { get; }

        public MarketEventModel(object payload)
            : this(CreateMetadata(payload), JObject.FromObject(payload))
        {
        }

        [JsonConstructor]
        public MarketEventModel(MetadataModel metadata, JObject payload)
        {
            Metadata = metadata;
            Payload = payload;
        }

        public T PayloadAs<T>() => Payload.ToObject<T>();

        private static MetadataModel CreateMetadata(object payload)
        {
            return new MetadataModel
            {
                Timestamp = DateTimeOffset.UtcNow,
                EventType = payload.ToMarketEventType()
            };
        }
    }
}