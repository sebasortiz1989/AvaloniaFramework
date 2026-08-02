using System;
using System.Runtime.Serialization;

namespace AvaloniaFramework.Presentation.View.UseCase;

[Serializable]
public sealed class PresenterCanceledException
    : PresenterException
{
    public PresenterCanceledException()
        : base(Messages.PresenterCanceledExceptionMessage)
    {
    }

    private PresenterCanceledException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}