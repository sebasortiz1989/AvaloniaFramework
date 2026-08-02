namespace AvaloniaFramework.Presentation.View.UseCase;

public readonly struct LifecycleRunResult<TResult>
{
    public readonly bool IsExited;
    public readonly bool IsPopped;
    public readonly TResult Result;

    public LifecycleRunResult(TResult result, bool isPopped, bool isExited)
    {
        IsExited = isExited;
        IsPopped = isPopped;
        Result = result;
    }

    public bool IsCompletedSuccessful => !IsExited && !IsPopped;
}

public static class LifecycleRunResult
{
    public static LifecycleRunResult<TResult> ExitedResult<TResult>() =>
        new LifecycleRunResult<TResult>(default, false, true);

    public static LifecycleRunResult<TResult> FromResult<TResult>(TResult value, bool isPopped = false) =>
        new LifecycleRunResult<TResult>(value, isPopped, false);

    public static LifecycleRunResult<TResult> PoppedResult<TResult>() =>
        new LifecycleRunResult<TResult>(default, true, false);
}