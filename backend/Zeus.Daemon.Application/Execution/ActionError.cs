namespace Zeus.Daemon.Application.Execution;

public record ActionError
{
    public required string Message { get; init; }
    public object? Details { get; init; }
    public Exception? InnerException { get; init; }
}
