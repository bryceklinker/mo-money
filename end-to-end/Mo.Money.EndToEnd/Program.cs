using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Mo.Money.EndToEnd.Application;
using Mo.Money.EndToEnd.Common;
using Serilog;

namespace Mo.Money.EndToEnd
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.ApplicationInsightsEvents("")
                .WriteTo.ApplicationInsightsTraces("")
                .CreateLogger();
            
            var provider = new ServiceCollection().AddEndToEndServices().BuildServiceProvider();
            var manager = provider.GetRequiredService<ApplicationManager>();
            await manager.StartAsync();

            var cypressProcess = Bash.Execute("yarn test");
            cypressProcess.WaitForExit();

            await manager.StopAsync();
        }
    }
}