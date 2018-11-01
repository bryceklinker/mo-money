using System;
using Market.Simulator.Client.Common;

namespace Market.Simulator.Client.Quotes
{
    public class QuoteModel: IModel
    {
        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Price { get; set; }
    }
}