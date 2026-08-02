using System;
using System.Threading;

namespace AvaloniaFramework.Presentation.View;

public readonly struct PresentationExecutionContext
    : IEquatable<PresentationExecutionContext>
{
    public readonly MessageDialog MessageDialog;
    public readonly SynchronizationContext Synchronization;

    public PresentationExecutionContext(MessageDialog messageDialog, SynchronizationContext synchronization)
    {
        MessageDialog = messageDialog;
        Synchronization = synchronization;
    }

    public static bool operator ==(PresentationExecutionContext left, PresentationExecutionContext right) =>
        left.Equals(right);

    public static bool operator !=(PresentationExecutionContext left, PresentationExecutionContext right) =>
        !left.Equals(right);

    public bool Equals(PresentationExecutionContext other)
    {
        return Equals(MessageDialog, other.MessageDialog) &&
               Equals(Synchronization, other.Synchronization);
    }

    public override bool Equals(object obj)
    {
        return obj is PresentationExecutionContext other && Equals(other);
    }

    public override int GetHashCode()
    {
        return MessageDialog?.GetHashCode() ?? Synchronization?.GetHashCode() ?? 0;
    }
}