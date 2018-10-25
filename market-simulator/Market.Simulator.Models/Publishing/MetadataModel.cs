using System;

namespace Market.Simulator.Models.Publishing
{
    public class MetadataModel
    {
        public DateTimeOffset Timestamp { get; set; }
        public MarketEventType EventType { get; set; } 
    }
}