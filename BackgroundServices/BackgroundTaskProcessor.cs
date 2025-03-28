using DataDrivenApi.Services.TaskQueue;

namespace DataDrivenApi.BackgroundServices;

public class BackgroundTaskProcessor : BackgroundService
{
    private readonly IBackgroundTaskQueue _taskQueue;
    private readonly ILogger<BackgroundTaskProcessor> _logger;

    public BackgroundTaskProcessor(
        IBackgroundTaskQueue taskQueue,
        ILogger<BackgroundTaskProcessor> logger)
    {
        _taskQueue = taskQueue;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Task Processor is running.");

        await BackgroundProcessing(stoppingToken);
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var task = await _taskQueue.DequeueAsync(stoppingToken);
                task.Status = Models.BackgroundTask.TaskStatus.Processing;

                _logger.LogInformation("Processing task {TaskId}", task.Id);

                // Simulate processing (replace with actual work)
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

                // Simulate successful completion
                task.Status = Models.BackgroundTask.TaskStatus.Completed;
                task.CompletedAt = DateTime.UtcNow;
                task.Result = $"Processed at {DateTime.UtcNow}";

                _logger.LogInformation("Completed task {TaskId}", task.Id);
            }
            catch (OperationCanceledException)
            {
                // Prevent throwing if stoppingToken was signaled
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred executing task");
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Background Task Processor is stopping.");

        await base.StopAsync(stoppingToken);
    }
}