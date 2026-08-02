using System.Threading.Tasks;

namespace AvaloniaFramework.Presentation.View;

public enum EventLevel
{
    Trace,
    Debug,
    Information,
    Warning,
    Error,
    Fatal,
}

public enum MessageDialogResult
{
    None,
    Ok,
    Yes,
    No,
    Cancel,
}

public interface MessageDialog
{
    Task<MessageDialogResult> ShowAsync(
        string message,
        string title = null,
        EventLevel severity = EventLevel.Information,
        MessageDialogResult defaultResult = MessageDialogResult.None);

    Task<MessageDialogResult> ShowQuestionAsync(
        string message,
        string title = null,
        MessageDialogResult defaultResult = MessageDialogResult.None);

    Task<MessageDialogResult> ShowCancelableQuestionAsync(
        string message,
        string title = null,
        MessageDialogResult defaultResult = MessageDialogResult.None);
}