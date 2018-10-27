using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Market.Simulator.Models.Publishing
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketEventType
    {
        Unknown,
        NewQuote,
        NewCompany,
        CompanyUpdate
    }
}