using Microsoft.Extensions.Logging;

namespace Zeus.Daemon.Application.Execution;

using System;

public class AutomationExecutionLogger : ILogger
{
    private readonly Guid _id;
    private string? _scope;

    public AutomationExecutionLogger(Guid id)
    {
        _id = id;
    }

    public IDisposable BeginScope<TState>(TState state) where TState : notnull => null!;

    public void WithScope(string scope)
    {
        _scope = scope;
    }

    public void ResetScope()
    {
        _scope = null;
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        (ConsoleColor initialBg, ConsoleColor initialText) = (Console.BackgroundColor, Console.ForegroundColor);
        (string label, ConsoleColor bgColor, ConsoleColor textColor) = logLevel switch
        {
            LogLevel.Trace => ("trace", ConsoleColor.Gray, ConsoleColor.Black),
            LogLevel.Debug => ("debug", ConsoleColor.Gray, ConsoleColor.Black),
            LogLevel.Information => ("info", ConsoleColor.DarkGreen, ConsoleColor.Black),
            LogLevel.Warning => ("warn", ConsoleColor.Yellow, ConsoleColor.Black),
            LogLevel.Error => ("fail", ConsoleColor.DarkRed, ConsoleColor.Black),
            LogLevel.Critical => ("crit", ConsoleColor.Red, ConsoleColor.White),
            _ => ("???", ConsoleColor.Black, ConsoleColor.Black)
        };

        Console.BackgroundColor = bgColor;
        Console.ForegroundColor = textColor;

        Console.Write(label);

        Console.BackgroundColor = initialBg;
        Console.ForegroundColor = initialText;


        Console.Write($": Automation execution {_id}: {DateTime.Now:u}");
        if (!string.IsNullOrEmpty(_scope))
        {
            Console.Write($" - {_scope}");
        }
        Console.WriteLine();
        Console.WriteLine(message);
    }
}
