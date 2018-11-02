using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Mo.Money.EndToEnd.Application
{
    public class ApplicationManager
    {
        private readonly ApplicationServer[] _applications;

        public ApplicationManager(ILoggerFactory loggerFactory)
        {
            _applications = Applications.GetAll()
                .Select(a => new ApplicationServer(a, loggerFactory))
                .ToArray();
        }
        
        public async Task StartAsync()
        {
            var tasks = _applications.Select(a => a.StartAsync());
            await Task.WhenAll(tasks);
        }

        public async Task StopAsync()
        {
            var tasks = _applications.Select(a => a.StopAsync());
            await Task.WhenAll(tasks);
        }
    }
}