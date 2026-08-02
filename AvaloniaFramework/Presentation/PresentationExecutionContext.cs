namespace AvaloniaFramework.Presentation;

/// <summary>
/// Ambient services handed to a presentation model for the duration of one run. Created by the
/// <see cref="NavigationController"/> and passed down through
/// <see cref="UseCase.LifecycleStep{TInput, TResult}.RunAsync"/>.
/// </summary>
public readonly struct PresentationExecutionContext(SynchronizationContext synchronization)
    : IEquatable<PresentationExecutionContext>
{
    /// <summary>The UI thread's context, for marshalling work that touches bound state.</summary>
    public SynchronizationContext Synchronization { get; } = synchronization;

    public static bool operator ==(PresentationExecutionContext left, PresentationExecutionContext right) =>
        left.Equals(right);

    public static bool operator !=(PresentationExecutionContext left, PresentationExecutionContext right) =>
        !left.Equals(right);

    public bool Equals(PresentationExecutionContext other) => Equals(Synchronization, other.Synchronization);

    public override bool Equals(object? obj) => obj is PresentationExecutionContext other && Equals(other);

    public override int GetHashCode() => Synchronization?.GetHashCode() ?? 0;
}
