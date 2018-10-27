using Market.Simulator.Models.Publishing;
using Market.Simulator.Models.Quotes;

namespace Market.Simulator.Models
{
    internal static class ObjectExtensions
    {
        public static MarketEventType ToMarketEventType(this object instance)
        {
            if (instance is QuoteModel)
                return MarketEventType.NewQuote;

            return MarketEventType.Unknown;
        } 
    }
}