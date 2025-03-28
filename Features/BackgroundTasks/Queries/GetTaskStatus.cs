using DataDrivenApi.Models.BackgroundTask;
using DataDrivenApi.Services.TaskQueue;
using MediatR;

namespace DataDrivenApi.Features.BackgroundTasks.Queries;

public record GetTaskStatusQuery(Guid TaskId) : IRequest<BackgroundTask?>;

public class GetTaskStatusQueryHandler : IRequestHandler<GetTaskStatusQuery, BackgroundTask?>
{
    private readonly IBackgroundTaskQueue _taskQueue;

    public GetTaskStatusQueryHandler(IBackgroundTaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
    }

    public async Task<BackgroundTask?> Handle(GetTaskStatusQuery request, CancellationToken cancellationToken)
    {
        return await _taskQueue.GetTaskStatusAsync(request.TaskId);
    }
}