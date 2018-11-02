using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mo.Money.EndToEnd.Common;

namespace Mo.Money.EndToEnd.Application
{
    public class ApplicationServer
    {
        private readonly ApplicationModel _model;
        private readonly ILogger _logger;
        private Process _process;
        private bool _hasStarted;

        public ApplicationServer(ApplicationModel model, ILoggerFactory loggerFactory)
        {
            _model = model;
            _logger = loggerFactory.CreateLogger(model.Name);
        }

        public async Task StartAsync()
        {
            LogInformation("Starting...");
            var command = $"dotnet run --urls=https://localhost:{_model.Port}";
            _process = Bash.Execute(command, _model.Path);
            _process.OutputDataReceived += OnOutput;
            _process.ErrorDataReceived += OnError; 
            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();

            while (!_hasStarted)
                await Task.Delay(200);
            LogInformation("Started");
        }

        public Task StopAsync()
        {
            if (_process == null || _process.HasExited)
                return Task.CompletedTask;

            LogInformation("Stopping...");
            _process.Kill();
            LogInformation("Stopped");
            return Task.CompletedTask;
        }

        private void OnOutput(object sender, DataReceivedEventArgs args)
        {
            if (args.Data.Contains("Now listening on:"))
            {
                _hasStarted = true;
            }
            
            LogInformation(args.Data);
        }

        private void OnError(object sender, DataReceivedEventArgs args)
        {
            LogError(args.Data);
        }

        private void LogInformation(string message)
        {
            _logger.LogInformation($"{_model.Name}: {message}");
        }
        
        private void LogError(string message)
        {
            _logger.LogError($"{_model.Name}: {message}");
        }
    }
}