namespace Zeus.Daemon.Application.Execution;

public class ActionRunningException : Exception
{
    public object? Details { get; }

    public ActionRunningException(string message, object? details = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Details = details;
    }
}
