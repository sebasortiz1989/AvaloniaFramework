using AvaloniaFramework.Hosting.Navigation;
using AvaloniaFramework.Presentation;
using AvaloniaFramework.Presentation.UseCase;
using Xunit;

namespace AvaloniaFramework.Tests;

/// <summary>
/// Stack identity and abandonment: Root/Dispose/PopToRoot must not leave awaiting PushAsync hung,
/// and PopAsync must refuse a model that is not on top.
/// </summary>
public class AvaloniaNavigationControllerTests
{
    private sealed class Screen : PresentationModelBase<string, int>
    {
        protected override Task OnRunStarting(string input) => Task.CompletedTask;

        // Default OnRunFinishing would Dispose — fine for these short-lived test screens, but keep
        // the override so a future Dispose override cannot surprise the suite.
        protected override Task OnRunFinishing() => Task.CompletedTask;
    }

    private sealed class FakePresenter(Screen model) : PresenterBase<Screen, string, int>
    {
        public bool Hosts(object candidate) => ReferenceEquals(model, candidate);

        public bool AbandonRun() => model.Abandon();

        public Task<int> RunAsync(
            string input,
            PresentationExecutionContext context,
            CancellationToken cancellationToken = default) =>
            model.RunAsync(input, context, cancellationToken);
    }

    [Fact]
    public async Task PopAsyncRefusesAModelThatIsNotOnTop()
    {
        var controller = new AvaloniaNavigationController(SynchronizationContext.Current!);
        var bottom = new Screen();
        var top = new Screen();

        var bottomRun = controller.PushAsync(new FakePresenter(bottom), "bottom");
        var topRun = controller.PushAsync(new FakePresenter(top), "top");

        Assert.Equal(2, controller.NavigationStackCount);
        Assert.False(await controller.PopAsync(bottom, 1));
        Assert.Equal(2, controller.NavigationStackCount);

        Assert.True(await controller.PopAsync(top, 7));
        Assert.Equal(7, await topRun);
        Assert.Equal(1, controller.NavigationStackCount);

        Assert.True(await controller.PopAsync(bottom, 3));
        Assert.Equal(3, await bottomRun);
    }

    [Fact]
    public async Task RootAsyncCancelsAbandonedRunsSoAwaitersDoNotHang()
    {
        var controller = new AvaloniaNavigationController(SynchronizationContext.Current!);
        var first = new Screen();
        var replacement = new Screen();

        var firstRun = controller.PushAsync(new FakePresenter(first), "first");
        Assert.Equal(1, controller.NavigationStackCount);

        var rootRun = controller.RootAsync(new FakePresenter(replacement), "root");

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => firstRun);
        Assert.Equal(1, controller.NavigationStackCount);

        Assert.True(await controller.PopAsync(replacement, 9));
        Assert.Equal(9, await rootRun);
    }

    [Fact]
    public async Task PopToRootAsyncCancelsEverythingAboveTheBottom()
    {
        var controller = new AvaloniaNavigationController(SynchronizationContext.Current!);
        var bottom = new Screen();
        var middle = new Screen();
        var top = new Screen();

        var bottomRun = controller.PushAsync(new FakePresenter(bottom), "bottom");
        var middleRun = controller.PushAsync(new FakePresenter(middle), "middle");
        var topRun = controller.PushAsync(new FakePresenter(top), "top");

        await controller.PopToRootAsync();

        Assert.Equal(1, controller.NavigationStackCount);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => middleRun);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => topRun);

        Assert.True(await controller.PopAsync(bottom, 1));
        Assert.Equal(1, await bottomRun);
    }

    [Fact]
    public async Task DisposeCancelsEveryOpenRun()
    {
        var controller = new AvaloniaNavigationController(SynchronizationContext.Current!);
        var first = new Screen();
        var second = new Screen();

        var firstRun = controller.PushAsync(new FakePresenter(first), "a");
        var secondRun = controller.PushAsync(new FakePresenter(second), "b");

        controller.Dispose();

        Assert.Equal(0, controller.NavigationStackCount);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => firstRun);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => secondRun);
        await Assert.ThrowsAsync<ObjectDisposedException>(
            () => controller.PushAsync(new FakePresenter(new Screen()), "after"));
    }

    [Fact]
    public void AbandonCancelsAnOpenRun()
    {
        var screen = new Screen();
        var run = screen.RunAsync("x", default);

        Assert.True(screen.Abandon());
        Assert.False(screen.Abandon());

        Assert.ThrowsAny<OperationCanceledException>(() => run.GetAwaiter().GetResult());
    }
}
