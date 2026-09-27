namespace AvaloniaFramework.Presentation;

/// <summary>What a <see cref="SynchronizedCommand.Faulted"/> handler is told: the exception a target threw.</summary>
public sealed class CommandFaultedEventArgs(Exception exception) : EventArgs
{
    /// <summary>The exception the target threw.</summary>
    public Exception Exception { get; } = exception ?? throw new ArgumentNullException(nameof(exception));
}