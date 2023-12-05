using Helper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuditLib
{
    public sealed class QueuedHostedService : BackgroundService
    {

        private readonly ILogger<QueuedHostedService> _logger;
        private readonly IAuditBackgroundTaskQueue _taskQueue;

        public QueuedHostedService(IAuditBackgroundTaskQueue taskQueue, ILogger<QueuedHostedService> logger)
        {
            _taskQueue = taskQueue;
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
        }


        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning("Queued Hosted Service is stopping.");
            while (!_taskQueue.IsEmpty())
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);
                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred executing {WorkItem}.", nameof(workItem));
                }
            }
            await base.StopAsync(stoppingToken);
        }
    }
}
