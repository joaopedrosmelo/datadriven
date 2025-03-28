using System.Collections.Concurrent;
using DataDrivenApi.Models.BackgroundTask;

namespace DataDrivenApi.Services.TaskQueue;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly ConcurrentQueue<BackgroundTask> _workItems = new();
    private readonly ConcurrentDictionary<Guid, BackgroundTask> _taskStore = new();
    private readonly SemaphoreSlim _signal = new(0);

    public async ValueTask QueueBackgroundWorkItemAsync(BackgroundTask task)
    {
        if (task == null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        _taskStore[task.Id] = task;
        _workItems.Enqueue(task);
        _signal.Release();

        await Task.CompletedTask;
    }

    public async ValueTask<BackgroundTask> DequeueAsync(CancellationToken cancellationToken)
    {
        await _signal.WaitAsync(cancellationToken);
        _workItems.TryDequeue(out var workItem);

        return workItem!;
    }

    public async ValueTask<BackgroundTask?> GetTaskStatusAsync(Guid taskId)
    {
        await Task.CompletedTask;
        return _taskStore.TryGetValue(taskId, out var task) ? task : null;
    }
}