using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DotNet5.Service.Exp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null)
            {
                var pathToExe = processModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(config =>
                {
                    config.AddCommandLine(args);
                    config.AddEnvironmentVariables(prefix: "DOTNETCORE_");
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile(path: $"{Directory.GetCurrentDirectory()}\\appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile(path: $"{Directory.GetCurrentDirectory()}\\appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging((hostContext, logging) =>
                {
                    Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(hostContext.Configuration)
                         .CreateLogger();

                    logging.AddSerilog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
