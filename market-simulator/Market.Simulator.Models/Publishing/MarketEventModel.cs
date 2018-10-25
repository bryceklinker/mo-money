using System;
using Newtonsoft.Json.Linq;

namespace Market.Simulator.Models.Publishing
{
    public class MarketEventModel
    {
        public MetadataModel Metadata { get; set; }
        internal JObject Raw { get; }

        public MarketEventModel(object raw)
        {
            Metadata = new MetadataModel
            {
                Timestamp = DateTimeOffset.UtcNow,
                EventType = raw.ToMarketEventType()
            };
            Raw = JObject.FromObject(raw);
        }

        public T GetPayload<T>() => Raw.ToObject<T>();
    }
}