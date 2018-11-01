using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Market.Simulator.Client.Publishing
{
    public class MarketEventModel
    {
        public MetadataModel Metadata { get; }
        public JObject Payload { get; }

        public MarketEventModel(object payload, MarketEventType? marketEventType = null)
            : this(CreateMetadata(payload, marketEventType), JObject.FromObject(payload))
        {
        }

        [JsonConstructor]
        public MarketEventModel(MetadataModel metadata, JObject payload)
        {
            Metadata = metadata;
            Payload = payload;
        }

        public T PayloadAs<T>() => Payload.ToObject<T>();

        private static MetadataModel CreateMetadata(object payload, MarketEventType? marketEventType = null)
        {
            return new MetadataModel
            {
                Timestamp = DateTimeOffset.UtcNow,
                EventType = marketEventType ?? payload.ToMarketEventType()
            };
        }
    }
}