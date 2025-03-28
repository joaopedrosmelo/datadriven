using DataDrivenApi.Models.BackgroundTask;
using DataDrivenApi.Services.TaskQueue;
using MediatR;

namespace DataDrivenApi.Features.BackgroundTasks.Commands;

public record SubmitBackgroundTaskCommand(string Description) : IRequest<Guid>;

public class SubmitBackgroundTaskCommandHandler : IRequestHandler<SubmitBackgroundTaskCommand, Guid>
{
    private readonly IBackgroundTaskQueue _taskQueue;

    public SubmitBackgroundTaskCommandHandler(IBackgroundTaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
    }

    public async Task<Guid> Handle(SubmitBackgroundTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new BackgroundTask { Description = request.Description };
        await _taskQueue.QueueBackgroundWorkItemAsync(task);
        return task.Id;
    }
}