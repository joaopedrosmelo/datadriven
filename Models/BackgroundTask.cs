namespace DataDrivenApi.Models.BackgroundTask;

public enum TaskStatus
{
    Queued,
    Processing,
    Completed,
    Failed
}

public class BackgroundTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Queued;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public string? Result { get; set; }
    public string? Error { get; set; }
}