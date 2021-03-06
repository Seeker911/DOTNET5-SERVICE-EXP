using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNet5.Service.Exp
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(15000, stoppingToken);
            }
        }

        public override Task StopAsync(System.Threading.CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker - StopAsync received");

            return base.StopAsync(stoppingToken);
        }

        public override Task StartAsync(System.Threading.CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Worker - StartAsync received");

            return base.StartAsync(stoppingToken);
        }
    }
}
