using AvaloniaFramework.Presentation;
using AvaloniaFramework.Presentation.UseCase;
using Xunit;

namespace AvaloniaFramework.Tests;

/// <summary>
/// The presenter lifecycle: a run begins when the model is handed its input and does not complete
/// until something finishes it, which is what makes <c>await PushAsync(...)</c> read as "show this
/// screen and give me its result".
/// </summary>
public class PresentationModelBaseTests
{
    private sealed class Screen : PresentationModelBase<string, int>
    {
        public List<string> Started { get; } = [];

        public int FinishingCalls { get; private set; }

        public bool WasCancelledWhileRunning { get; private set; }

        protected override Task OnRunStarting(string input)
        {
            Started.Add(input);
            WasCancelledWhileRunning = PresentationModelFinished.IsCancellationRequested;
            return Task.CompletedTask;
        }

        protected override Task OnRunFinishing()
        {
            FinishingCalls++;
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task ARunStaysOpenUntilItIsFinished()
    {
        var screen = new Screen();

        var run = screen.RunAsync("hello", default);

        Assert.False(run.IsCompleted);
        Assert.True(await screen.Finish(42));
        Assert.Equal(42, await run);
    }

    [Fact]
    public async Task TheInputReachesOnRunStarting()
    {
        var screen = new Screen();

        var run = screen.RunAsync("payload", default);
        await screen.Finish(0);
        await run;

        Assert.Equal(["payload"], screen.Started);
    }

    [Fact]
    public async Task TeardownRunsAfterTheResultArrives()
    {
        var screen = new Screen();

        var run = screen.RunAsync("hello", default);
        Assert.Equal(0, screen.FinishingCalls);

        await screen.Finish(1);
        await run;

        Assert.Equal(1, screen.FinishingCalls);
    }

    [Fact]
    public async Task BothLifecycleEventsAreRaisedInOrder()
    {
        var screen = new Screen();
        var order = new List<string>();
        screen.OnRunStarted += (_, _) => order.Add("started");
        screen.OnRunFinished += (_, _) => order.Add("finished");

        var run = screen.RunAsync("hello", default);
        await screen.Finish(1);
        await run;

        Assert.Equal(["started", "finished"], order);
    }

    [Fact]
    public async Task FinishingWithAnErrorFaultsTheRun()
    {
        var screen = new Screen();

        var run = screen.RunAsync("hello", default);
        Assert.True(await screen.FinishWithError(new InvalidOperationException("boom")));

        var error = await Assert.ThrowsAsync<InvalidOperationException>(() => run);
        Assert.Equal("boom", error.Message);
    }

    /// <summary>Finishing a screen that is not showing is a no-op, not an error.</summary>
    [Fact]
    public async Task FinishingWhenNothingIsRunningReportsFalse()
    {
        var screen = new Screen();

        Assert.False(await screen.Finish(1));
        Assert.False(await screen.FinishWithError(new InvalidOperationException()));
    }

    /// <summary>One model cannot show twice at once — the second push has to be refused.</summary>
    [Fact]
    public async Task RunningAModelThatIsAlreadyRunningThrows()
    {
        var screen = new Screen();
        var run = screen.RunAsync("first", default);

        await Assert.ThrowsAsync<InvalidOperationException>(() => screen.RunAsync("second", default));

        await screen.Finish(0);
        await run;
    }

    /// <summary>Once finished the model is reusable, which is what lets a tab presenter be reopened.</summary>
    [Fact]
    public async Task AModelCanRunAgainOnceItHasFinished()
    {
        var screen = new Screen();

        var first = screen.RunAsync("first", default);
        await screen.Finish(1);
        Assert.Equal(1, await first);

        var second = screen.RunAsync("second", default);
        await screen.Finish(2);
        Assert.Equal(2, await second);

        Assert.Equal(["first", "second"], screen.Started);
    }

    /// <summary>The token is live during the run, so work started there is not cancelled at birth.</summary>
    [Fact]
    public async Task TheFinishedTokenIsNotCancelledWhileTheRunIsOpen()
    {
        var screen = new Screen();

        var run = screen.RunAsync("hello", default);
        await screen.Finish(0);
        await run;

        Assert.False(screen.WasCancelledWhileRunning);
    }

    /// <summary>A caller-supplied token cancels the run rather than leaving it hanging.</summary>
    [Fact]
    public async Task CancellingTheCallersTokenCancelsTheRun()
    {
        var screen = new Screen();
        using var cancellation = new CancellationTokenSource();

        var run = screen.RunAsync("hello", default, cancellation.Token);
        await cancellation.CancelAsync();

        // The run ends one way or another rather than hanging; either shape is acceptable here.
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => run);
    }
}