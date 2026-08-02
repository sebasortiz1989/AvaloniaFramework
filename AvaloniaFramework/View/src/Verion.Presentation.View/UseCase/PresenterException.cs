using System;
using System.Runtime.Serialization;

namespace AvaloniaFramework.Presentation.View.UseCase;

[Serializable]
public class PresenterException
    : Exception
{
    public PresenterException()
        : base(Messages.PresenterExceptionDefaultMessage)
    {
    }

    public PresenterException(string message)
        : base(message)
    {
    }

    protected PresenterException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}