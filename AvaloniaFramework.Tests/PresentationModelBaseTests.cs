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

    /// <summary>
    /// The caller's token is cooperative: it surfaces through PresentationModelFinished so work
    /// inside the model can stop, but it does not complete the run on its own.
    /// </summary>
    /// <remarks>
    /// Worth pinning down, because the signature reads as though cancelling would end the run.
    /// It does not — only Finish, FinishWithError or Cancel do — so a caller who cancels and then
    /// awaits without finishing waits forever. The wait here is bounded so a regression shows up
    /// as a failure rather than a hung test run.
    /// </remarks>
    [Fact]
    public async Task CancellingTheCallersTokenDoesNotOnItsOwnEndTheRun()
    {
        var screen = new Screen();
        using var cancellation = new CancellationTokenSource();

        var run = screen.RunAsync("hello", default, cancellation.Token);
        await cancellation.CancelAsync();

        var finishedEarly = await Task.WhenAny(run, Task.Delay(TimeSpan.FromMilliseconds(250)));
        Assert.NotSame(run, finishedEarly);

        // Finishing is what actually ends it.
        await screen.Finish(7);
        Assert.Equal(7, await run);
    }
}