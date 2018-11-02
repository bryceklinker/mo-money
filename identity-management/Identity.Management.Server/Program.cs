using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Mo.Money.Common;

namespace Identity.Management.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureForMoMoney(commandLineArgs: args)
                .UseStartup<Startup>();
    }
}