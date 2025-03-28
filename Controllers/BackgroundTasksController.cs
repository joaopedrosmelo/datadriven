using DataDrivenApi.Features.BackgroundTasks.Commands;
using DataDrivenApi.Features.BackgroundTasks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DataDrivenApi.Controllers;

[ApiController]
[Route("api/background-tasks")]
public class BackgroundTasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BackgroundTasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitTask([FromBody] string description)
    {
        var command = new SubmitBackgroundTaskCommand(description);
        var taskId = await _mediator.Send(command);
        return Accepted(new { TaskId = taskId });
    }

    [HttpGet("{taskId}")]
    public async Task<IActionResult> GetTaskStatus(Guid taskId)
    {
        var query = new GetTaskStatusQuery(taskId);
        var task = await _mediator.Send(query);

        return task != null
            ? Ok(task)
            : NotFound();
    }
}