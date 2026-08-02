namespace AvaloniaFramework.Presentation;

/// <summary>What a <see cref="SynchronizedCommand"/> does when it is invoked while already running.</summary>
public enum SynchronizationBehavior
{
    /// <summary>Queue the invocation and run it once the current one finishes.</summary>
    Enqueue,

    /// <summary>Drop the invocation. The right choice for buttons, where a double tap is an accident.</summary>
    Discard,
}

/// <summary>
/// A shared lock. Pass the same gate to several commands to make them mutually exclusive — useful
/// when two buttons must not run at once even though either alone is re-entrant.
/// </summary>
public sealed class SynchronizationGate;
