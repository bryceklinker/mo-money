using Market.Simulator.Client.Publishing;
using Market.Simulator.Client.Quotes;

namespace Market.Simulator.Client
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