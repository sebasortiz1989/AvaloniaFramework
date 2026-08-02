using AvaloniaFramework.Presentation.UseCase;

namespace AvaloniaFramework.Presentation;

/// <summary>
/// The screen stack. Pushing a presenter shows it and returns a task that completes with the
/// screen's result once it is popped, so a caller can await a whole screen the way it would await
/// a dialog.
/// </summary>
public interface NavigationController : IDisposable
{
    /// <summary>The presenter currently on top, or null when the stack is empty.</summary>
    PresenterHandle? CurrentPresenter { get; }

    /// <summary>How many presenters are on the stack.</summary>
    int NavigationStackCount { get; }

    /// <summary>Creates the context handed to presenters this controller runs.</summary>
    PresentationExecutionContext CreateContext();

    /// <summary>Pops the top screen and finishes <paramref name="model"/>. False if it is not on top.</summary>
    Task<bool> PopAsync(PresentationModelBase<Unit, Unit> model);

    /// <inheritdoc cref="PopAsync(PresentationModelBase{Unit, Unit})" />
    Task<bool> PopAsync<TInput>(PresentationModelBase<TInput, Unit> model);

    /// <inheritdoc cref="PopAsync(PresentationModelBase{Unit, Unit})" />
    Task<bool> PopAsync<TOutput>(PresentationModelBase<Unit, TOutput> model, TOutput output);

    /// <inheritdoc cref="PopAsync(PresentationModelBase{Unit, Unit})" />
    Task<bool> PopAsync<TInput, TOutput>(PresentationModelBase<TInput, TOutput> model, TOutput output);

    /// <summary>Shows <paramref name="presenter"/> and completes when it is popped.</summary>
    Task<Unit> PushAsync<TModel>(PresenterBase<TModel, Unit, Unit> presenter)
        where TModel : PresentationModelBase<Unit, Unit>;

    /// <inheritdoc cref="PushAsync{TModel}(PresenterBase{TModel, Unit, Unit})" />
    Task<TOutput> PushAsync<TModel, TOutput>(PresenterBase<TModel, Unit, TOutput> presenter)
        where TModel : PresentationModelBase<Unit, TOutput>;

    /// <inheritdoc cref="PushAsync{TModel}(PresenterBase{TModel, Unit, Unit})" />
    Task<Unit> PushAsync<TModel, TInput>(PresenterBase<TModel, TInput, Unit> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, Unit>;

    /// <inheritdoc cref="PushAsync{TModel}(PresenterBase{TModel, Unit, Unit})" />
    Task<TOutput> PushAsync<TModel, TInput, TOutput>(PresenterBase<TModel, TInput, TOutput> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, TOutput>;

    /// <summary>Clears the stack and shows <paramref name="presenter"/> as the only entry.</summary>
    Task<Unit> RootAsync<TModel>(PresenterBase<TModel, Unit, Unit> presenter)
        where TModel : PresentationModelBase<Unit, Unit>;

    /// <inheritdoc cref="RootAsync{TModel}(PresenterBase{TModel, Unit, Unit})" />
    Task<TOutput> RootAsync<TModel, TOutput>(PresenterBase<TModel, Unit, TOutput> presenter)
        where TModel : PresentationModelBase<Unit, TOutput>;

    /// <inheritdoc cref="RootAsync{TModel}(PresenterBase{TModel, Unit, Unit})" />
    Task<Unit> RootAsync<TModel, TInput>(PresenterBase<TModel, TInput, Unit> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, Unit>;

    /// <inheritdoc cref="RootAsync{TModel}(PresenterBase{TModel, Unit, Unit})" />
    Task<TOutput> RootAsync<TModel, TInput, TOutput>(PresenterBase<TModel, TInput, TOutput> presenter, TInput input)
        where TModel : PresentationModelBase<TInput, TOutput>;

    /// <summary>Pops everything but the bottom entry.</summary>
    Task PopToRootAsync();
}
