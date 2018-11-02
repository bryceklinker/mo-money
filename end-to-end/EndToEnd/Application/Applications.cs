using System.IO;

namespace EndToEnd.Application
{
    public class Applications
    {
        private static readonly string SolutionRoot = Path.Combine("..", "..");

        private static readonly string IdentityServerPath =
            Path.Combine(SolutionRoot, "identity-management", "Identity.Management.Server");

        private static readonly string MarketSimulatorPath =
            Path.Combine(SolutionRoot, "market-simulator", "Market.Simulator.Server");
        
        private static readonly string StockTickerPath =
            Path.Combine(SolutionRoot, "stock-ticker", "Stock.Ticker.Server");
        
        private static readonly string PortalPath =
            Path.Combine(SolutionRoot, "portal", "Portal.Server");
        
        public static ApplicationModel GetIdentityManagement()
        {
            return new ApplicationModel("Identity Management", IdentityServerPath, 6000);
        }

        public static ApplicationModel GetMarketSimulator()
        {
            return new ApplicationModel("Market Simulator", MarketSimulatorPath, 6001);
        }

        public static ApplicationModel GetStockTicker()
        {
            return new ApplicationModel("Stock Ticker", StockTickerPath, 6002);
        }

        public static ApplicationModel GetPortal()
        {
            return new ApplicationModel("Portal", PortalPath, 6003);
        }

        public static ApplicationModel[] GetAll()
        {
            return new[]
            {
                GetIdentityManagement(),
                GetMarketSimulator(),
                GetStockTicker(),
                GetPortal()
            };
        }
    }
}