using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Text;

namespace Helper
{
    public static class Logger
    {
        public static IServiceCollection AddSerilog(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var logConfig = configuration.Get<LogConfig>() ?? new LogConfig();
            var minimumLevel = Enum.TryParse<Serilog.Events.LogEventLevel>(logConfig.MinimumLevel, true, out var result) ? result : Serilog.Events.LogEventLevel.Warning;
            serviceCollection.AddLogging(config =>
            {
                // clear out default configuration
                config.ClearProviders();
                var logger = new LoggerConfiguration()
                .MinimumLevel.Is(minimumLevel)
                .WriteTo.Async(a => a.File(Path.Combine(string.IsNullOrEmpty(logConfig.DirectoryPath) ? $"{AppContext.BaseDirectory}LOG/" : logConfig.DirectoryPath, $"{System.Net.Dns.GetHostName()}-log.txt"),
                        rollingInterval: RollingInterval.Day,
                        rollOnFileSizeLimit: true,
                        fileSizeLimitBytes: 209715200, //~200 mb
                        buffered: true,
                        encoding: Encoding.UTF8,
                        retainedFileCountLimit: null,
                        flushToDiskInterval: TimeSpan.FromSeconds(logConfig.FlushToDiskIntervalInSecond),
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{SourceContext}] [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                    ), bufferSize: 1000).CreateLogger();
                config.AddSerilog(logger, true);
                Log.Logger = logger;
            });
            return serviceCollection;
        }

        public class LogConfig
        {
            public const string Name = "Log";
            public string MinimumLevel { get; set; } = "Warning";
            public string DirectoryPath { get; set; } = string.Empty;
            public int FlushToDiskIntervalInSecond { get; set; } = 5;

        }
    }
}
