using DataDrivenApi.Models.BackgroundTask;

namespace DataDrivenApi.Services.TaskQueue;

public interface IBackgroundTaskQueue
{
    ValueTask QueueBackgroundWorkItemAsync(BackgroundTask task);
    ValueTask<BackgroundTask> DequeueAsync(CancellationToken cancellationToken);
    ValueTask<BackgroundTask?> GetTaskStatusAsync(Guid taskId);
}