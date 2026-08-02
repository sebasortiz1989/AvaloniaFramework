using System;
using System.Runtime.Serialization;

namespace AvaloniaFramework.Presentation.View.UseCase;

[Serializable]
public sealed class PresenterRewindException
    : PresenterException
{
    public PresenterRewindException()
        : base(Messages.PresenterRewindExceptionMessage)
    {
    }

    private PresenterRewindException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}