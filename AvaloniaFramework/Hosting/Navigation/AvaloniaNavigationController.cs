using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaFramework.Presentation;
using AvaloniaFramework.Presentation.UseCase;
using AvaloniaFramework.Threading;

namespace AvaloniaFramework.Hosting.Navigation;

/// <summary>
/// Drives the shell's content from a stack of presenters. Registered as a singleton by
/// <see cref="DependencyInjection.AvaloniaViewContainerBuilder"/>; take a
/// <see cref="NavigationController"/> in a view model's constructor to navigate.
/// </summary>
public sealed class AvaloniaNavigationController(SynchronizationContext synchronizationContext)
    : NavigationController
{
    private readonly Stack<PresenterHandle> navigationStack = new();
    private object? shellDefaultContent;
    private volatile bool closing;

    /// <summary>
    /// The shell on activity-based platforms. Android's <see cref="IActivityApplicationLifetime"/>
    /// exposes only a <c>MainViewFactory</c> and no live instance — an activity can be recreated,
    /// so there is nothing stable for the lifetime to hand back. The host therefore tells the
    /// controller which shell is current through <see cref="AttachShell"/>.
    /// </summary>
    private ContentControl? activityShell;

    /// <inheritdoc />
    public PresenterHandle? CurrentPresenter => navigationStack.TryPeek(out var value) ? value : null;

    /// <inheritdoc />
    public int NavigationStackCount => navigationStack.Count;

    /// <inheritdoc />
    public PresentationExecutionContext CreateContext() => new(synchronizationContext);

    /// <summary>
    /// Registers the shell an activity-based platform has just created, and immediately shows the
    /// current screen in it.
    /// </summary>
    /// <remarks>
    /// Call this from the <c>MainViewFactory</c> the host assigns to
    /// <see cref="IActivityApplicationLifetime"/>. Android runs the factory again whenever it
    /// recreates the activity — on rotation, or after the process is reclaimed — so the newest
    /// shell replaces the previous one and inherits the navigation stack, which lives here rather
    /// than in the view.
    /// </remarks>
    /// <param name="shell">The freshly created shell.</param>
    public void AttachShell(ContentControl shell)
    {
        ArgumentNullException.ThrowIfNull(shell);

        activityShell = shell;

        // Read per shell, not cached across them: a recreated activity brings a new instance whose
        // own placeholder content is what the stack should fall back to when it empties.
        shellDefaultContent = shell.Content;

        ShowCurrentPresenter();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        closing = true;
        shellDefaultContent = null;
        activityShell = null;
        navigationStack.Clear();
    }

    /// <inheritdoc />
    public Task<bool> PopAsync(PresentationModelBase<Unit, Unit> model) =>
        PopCore<Unit, Unit>(model, Unit.Value);

    /// <inheritdoc />
    public Task<bool> PopAsync<TInput>(PresentationModelBase<TInput, Unit> model) =>
        PopCore<TInput, Unit>(model, Unit.Value);

    /// <inheritdoc />
    public Task<bool> PopAsync<TOutput>(PresentationModelBase<Unit, TOutput> model, TOutput output) =>
        PopCore(model, output);

    /// <inheritdoc />
    public Task<bool> PopAsync<TInput, TOutput>(PresentationModelBase<TInput, TOutput> model, TOutput output) =>
        PopCore(model, output);

    /// <inheritdoc />
    public Task<Unit> PushAsync<TModel>(PresenterBase<TModel, Unit, Unit> presenter)
        where TModel : PresentationModelBase<Unit, Unit> =>
        PushCore(presenter, Unit.Value);

    /// <inheritdoc />
    public Task<TOutput> PushAsync<TModel, TOutput>(PresenterBase<TModel, Unit, TOutput> presenter)
        where TModel : PresentationModelBase<Unit, TOutput> =>
        PushCore(presenter, Unit.Value);

    /// <inheritdoc />
    public Task<Unit> PushAsync<TModel, TInput>(PresenterBase<TModel, TInput, Unit> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, Unit> =>
        PushCore(presenter, input);

    /// <inheritdoc />
    public Task<TOutput> PushAsync<TModel, TInput, TOutput>(PresenterBase<TModel, TInput, TOutput> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, TOutput> =>
        PushCore(presenter, input);

    /// <inheritdoc />
    public Task<Unit> RootAsync<TModel>(PresenterBase<TModel, Unit, Unit> presenter)
        where TModel : PresentationModelBase<Unit, Unit> =>
        RootCore(presenter, Unit.Value);

    /// <inheritdoc />
    public Task<TOutput> RootAsync<TModel, TOutput>(PresenterBase<TModel, Unit, TOutput> presenter)
        where TModel : PresentationModelBase<Unit, TOutput> =>
        RootCore(presenter, Unit.Value);

    /// <inheritdoc />
    public Task<Unit> RootAsync<TModel, TInput>(PresenterBase<TModel, TInput, Unit> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, Unit> =>
        RootCore(presenter, input);

    /// <inheritdoc />
    public Task<TOutput> RootAsync<TModel, TInput, TOutput>(PresenterBase<TModel, TInput, TOutput> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, TOutput> =>
        RootCore(presenter, input);

    /// <inheritdoc />
    public Task PopToRootAsync()
    {
        if (closing)
            return Task.CompletedTask;

        while (navigationStack.Count > 1)
            navigationStack.Pop();

        ShowCurrentPresenter();
        return Task.CompletedTask;
    }

    private async Task<TResult> PushCore<TInput, TResult>(
        LifecycleStep<TInput, TResult> presenter,
        TInput input)
    {
        ArgumentNullException.ThrowIfNull(presenter);

        if (!closing)
        {
            navigationStack.Push((PresenterHandle)presenter);
            ShowCurrentPresenter();
        }

        return await presenter.RunAsync(input, CreateContext(), CancellationToken.None).WithSync();
    }

    private async Task<TResult> RootCore<TInput, TResult>(
        LifecycleStep<TInput, TResult> presenter,
        TInput input)
    {
        ArgumentNullException.ThrowIfNull(presenter);

        if (!closing)
        {
            navigationStack.Clear();
            navigationStack.Push((PresenterHandle)presenter);
            ShowCurrentPresenter();
        }

        return await presenter.RunAsync(input, CreateContext(), CancellationToken.None).WithSync();
    }

    private async Task<bool> PopCore<TInput, TResult>(PresentationModelBase<TInput, TResult> model, TResult output)
    {
        ArgumentNullException.ThrowIfNull(model);

        if (navigationStack.Count == 0)
            return false;

        if (!closing)
        {
            navigationStack.Pop();
            ShowCurrentPresenter();
        }

        // Finishing the model is what completes the task the matching PushAsync returned.
        return await model.Finish(output).WithSync();
    }

    private void ShowCurrentPresenter()
    {
        switch (Application.Current?.ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime { MainWindow: { } mainWindow } desktop:
                ShowInDesktopShell(desktop, mainWindow);
                break;

            // Before the single-view case: Avalonia 12 moved Android onto
            // IActivityApplicationLifetime, and an implementation may satisfy both interfaces —
            // matching the single-view one first would look for a MainView that is never set.
            case IActivityApplicationLifetime when activityShell is { } shell:
                shell.Content = CurrentPresenter ?? shellDefaultContent;
                break;

            case ISingleViewApplicationLifetime { MainView: ContentControl mainView }:
                shellDefaultContent ??= mainView.Content;
                mainView.Content = CurrentPresenter ?? shellDefaultContent;
                break;
        }
    }

    private void ShowInDesktopShell(IClassicDesktopStyleApplicationLifetime desktop, Window mainWindow)
    {
        shellDefaultContent ??= mainWindow.Content;

        switch (CurrentPresenter)
        {
            case Window window:
                mainWindow.Hide();
                desktop.MainWindow = window;
                window.Show();
                window.Activate();
                return;

            case Control control:
                mainWindow.Content = control;
                break;

            default:
                mainWindow.Content = shellDefaultContent;
                break;
        }

        mainWindow.Activate();
    }
}
