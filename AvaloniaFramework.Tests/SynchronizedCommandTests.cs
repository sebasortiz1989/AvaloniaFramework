using AvaloniaFramework.Presentation;
using System.ComponentModel;
using System.Windows.Input;
using Xunit;

namespace AvaloniaFramework.Tests;

/// <summary>
/// The command that will not run twice at once — the thing standing between a double tap and the
/// same screen being pushed twice.
/// </summary>
public class SynchronizedCommandTests
{
    [Fact]
    public async Task ASynchronousHandlerRuns()
    {
        var ran = 0;
        using var command = new SynchronizedCommand(() => ran++, SynchronizationBehavior.Discard, true);

        await command.ExecuteAsync(null);

        Assert.Equal(1, ran);
    }

    [Fact]
    public async Task AnAsynchronousHandlerIsAwaited()
    {
        var finished = false;
        using var command = new SynchronizedCommand(
            async () =>
            {
                await Task.Yield();
                finished = true;
            },
            SynchronizationBehavior.Discard,
            true);

        await command.ExecuteAsync(null);

        Assert.True(finished);
    }

    [Fact]
    public async Task TheCommandParameterReachesTheHandler()
    {
        object? seen = null;
        using var command = new SynchronizedCommand(p => seen = p, SynchronizationBehavior.Discard, true);

        await command.ExecuteAsync("payload");

        Assert.Equal("payload", seen);
    }

    [Fact]
    public async Task ADisabledCommandDoesNotRun()
    {
        var ran = 0;
        using var command = new SynchronizedCommand(() => ran++, SynchronizationBehavior.Discard, false);

        await command.ExecuteAsync(null);

        Assert.Equal(0, ran);
        Assert.False(((ICommand)command).CanExecute(null));
    }

    /// <summary>
    /// The double-tap case. A second invocation arriving while the first is still running is
    /// dropped, so the handler runs once however many times the button is hit.
    /// </summary>
    [Fact]
    public async Task DiscardDropsAnInvocationThatArrivesMidFlight()
    {
        var runs = 0;
        var release = new TaskCompletionSource();
        SynchronizedCommand? command = null;

        command = new SynchronizedCommand(
            async () =>
            {
                runs++;

                // Re-entering while the first call is still in flight is exactly what a double
                // tap does.
                await command!.ExecuteAsync(null);
                await release.Task;
            },
            SynchronizationBehavior.Discard,
            true);

        var running = command.ExecuteAsync(null);
        release.SetResult();
        await running;

        Assert.Equal(1, runs);
        command.Dispose();
    }

    /// <summary>Enqueue keeps the second invocation and runs it once the first finishes.</summary>
    [Fact]
    public async Task EnqueueRunsTheQueuedInvocationAfterTheFirst()
    {
        var runs = 0;
        var release = new TaskCompletionSource();
        SynchronizedCommand? command = null;

        command = new SynchronizedCommand(
            async () =>
            {
                runs++;
                if (runs == 1)
                {
                    await command!.ExecuteAsync(null);
                    await release.Task;
                }
            },
            SynchronizationBehavior.Enqueue,
            true);

        var running = command.ExecuteAsync(null);
        release.SetResult();
        await running;

        Assert.Equal(2, runs);
        command.Dispose();
    }

    /// <summary>Once finished the command is free again — the gate is not a one-shot latch.</summary>
    [Fact]
    public async Task TheCommandCanRunAgainAfterItFinishes()
    {
        var runs = 0;
        using var command = new SynchronizedCommand(() => runs++, SynchronizationBehavior.Discard, true);

        await command.ExecuteAsync(null);
        await command.ExecuteAsync(null);
        await command.ExecuteAsync(null);

        Assert.Equal(3, runs);
    }

    [Fact]
    public void ChangingCanExecuteRaisesBothNotifications()
    {
        using var command = new SynchronizedCommand(() => { }, SynchronizationBehavior.Discard, true);

        var canExecuteChanged = 0;
        var propertyChanged = new List<string?>();
        command.CanExecuteChanged += (_, _) => canExecuteChanged++;
        ((INotifyPropertyChanged)command).PropertyChanged += (_, e) => propertyChanged.Add(e.PropertyName);

        command.CanExecute = false;

        Assert.Equal(1, canExecuteChanged);
        Assert.Equal([nameof(SynchronizedCommand.CanExecute)], propertyChanged);
    }

    /// <summary>Setting the same value is not a change, so bound controls are not re-queried.</summary>
    [Fact]
    public void SettingCanExecuteToItsCurrentValueRaisesNothing()
    {
        using var command = new SynchronizedCommand(() => { }, SynchronizationBehavior.Discard, true);

        var raised = 0;
        command.CanExecuteChanged += (_, _) => raised++;

        command.CanExecute = true;

        Assert.Equal(0, raised);
    }

    [Fact]
    public void ANullHandlerIsRejected()
    {
        Assert.Throws<ArgumentNullException>(() => new SynchronizedCommand((Action)null!, SynchronizationBehavior.Discard, true));
    }

    [Fact]
    public void AnUnknownBehaviourIsRejected()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => new SynchronizedCommand(() => { }, (SynchronizationBehavior)99, true));
    }

    /// <summary>
    /// A target that faults once does not kill the command. Before the fix the running
    /// flag was cleared only on the normal path, so this target ran once in three presses while
    /// <see cref="SynchronizedCommand.CanExecute"/> still said true.
    /// </summary>
    [Fact]
    public async Task ATargetThatFaultsOnceLeavesTheCommandRunnable()
    {
        var calls = 0;
        using var command = new SynchronizedCommand(
            async () =>
            {
                calls++;
                await Task.Yield();
                if (calls == 1)
                    throw new InvalidOperationException("first press faults");
            },
            SynchronizationBehavior.Discard,
            true);

        // The fault still reaches a caller that awaits.
        await Assert.ThrowsAsync<InvalidOperationException>(() => command.ExecuteAsync(null));

        await command.ExecuteAsync(null);
        await command.ExecuteAsync(null);

        Assert.Equal(3, calls);
    }

    /// <summary>
    /// The press a button makes goes through <see cref="ICommand.Execute"/>, which has
    /// nothing to await, so its fault arrives through <see cref="SynchronizedCommand.Faulted"/> —
    /// the same exception, once.
    /// </summary>
    [Fact]
    public async Task AFaultFromAButtonPressArrivesThroughFaulted()
    {
        var thrown = new InvalidOperationException("the press faults");
        var arrived = new List<Exception>();
        var done = new TaskCompletionSource();
        using var command = new SynchronizedCommand(
            async () =>
            {
                await Task.Yield();
                throw thrown;
            },
            SynchronizationBehavior.Discard,
            true);

        command.Faulted += (sender, e) =>
        {
            Assert.Same(command, sender);
            arrived.Add(e.Exception);
            done.TrySetResult();
        };

        ((ICommand)command).Execute(null);
        await done.Task.WaitAsync(TimeSpan.FromSeconds(5));

        Assert.Same(thrown, Assert.Single(arrived));
    }

    /// <summary>
    /// With a handler, the fault is not ALSO left in an unobserved task. That second
    /// report is where the fault went before — <see cref="TaskScheduler.UnobservedTaskException"/>
    /// fired and nothing reached the user — and a handled fault must not keep going there.
    /// </summary>
    [Fact]
    public async Task AHandledFaultIsNotAlsoLeftUnobserved()
    {
        var thrown = new InvalidOperationException("handled, and only handled");
        var unobserved = 0;

        void OnUnobserved(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            if (e.Exception.Flatten().InnerExceptions.Contains(thrown))
                Interlocked.Increment(ref unobserved);
        }

        TaskScheduler.UnobservedTaskException += OnUnobserved;
        try
        {
            var handled = new TaskCompletionSource();
            PressAndAbandon(thrown, handled);
            await handled.Task.WaitAsync(TimeSpan.FromSeconds(5));

            for (var pass = 0; pass < 5; pass++)
            {
                await Task.Delay(20);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            Assert.Equal(0, unobserved);
        }
        finally
        {
            TaskScheduler.UnobservedTaskException -= OnUnobserved;
        }
    }

    /// <summary>
    /// The nearest miss. A press queued behind the faulting one still runs, AND the
    /// command is released afterwards. Releasing without draining strands the queued press;
    /// draining without releasing kills the command — each half alone leaves one of these red.
    /// </summary>
    [Fact]
    public async Task APressQueuedBehindAFaultStillRunsAndTheCommandIsReleased()
    {
        var runs = 0;
        var faults = 0;
        SynchronizedCommand? command = null;

        command = new SynchronizedCommand(
            async () =>
            {
                runs++;
                if (runs == 1)
                {
                    await command!.ExecuteAsync(null);
                    throw new InvalidOperationException("the first press faults with a press queued behind it");
                }
            },
            SynchronizationBehavior.Enqueue,
            true);
        command.Faulted += (_, _) => faults++;

        await Assert.ThrowsAsync<InvalidOperationException>(() => command.ExecuteAsync(null));
        Assert.Equal(2, runs);

        await command.ExecuteAsync(null);
        Assert.Equal(3, runs);
        Assert.Equal(1, faults);
        command.Dispose();
    }

    /// <summary>
    /// A cancellation is not a fault: it releases the command like one, but is not
    /// raised through <see cref="SynchronizedCommand.Faulted"/>, so a screen does not report
    /// "could not complete" for something the user called off.
    /// </summary>
    [Fact]
    public async Task ACancelledTargetReleasesTheCommandAndIsNotReportedAsAFault()
    {
        var calls = 0;
        var faults = 0;
        using var command = new SynchronizedCommand(
            async () =>
            {
                calls++;
                await Task.Yield();
                if (calls == 1)
                    throw new OperationCanceledException();
            },
            SynchronizationBehavior.Discard,
            true);
        command.Faulted += (_, _) => faults++;

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => command.ExecuteAsync(null));
        await command.ExecuteAsync(null);

        Assert.Equal(2, calls);
        Assert.Equal(0, faults);
    }

    /// <summary>
    /// A <see cref="SynchronizedCommand.Faulted"/> handler that itself throws is the
    /// consumer's bug, and its exception is what the caller sees — but it must not re-open the
    /// defect by leaving the command running.
    /// </summary>
    [Fact]
    public async Task AThrowingFaultHandlerDoesNotLeaveTheCommandRunning()
    {
        var calls = 0;
        using var command = new SynchronizedCommand(
            () =>
            {
                calls++;
                if (calls == 1)
                    throw new InvalidOperationException("the target faults");
            },
            SynchronizationBehavior.Discard,
            true);
        command.Faulted += (_, _) => throw new NotSupportedException("the handler faults too");

        await Assert.ThrowsAsync<NotSupportedException>(() => command.ExecuteAsync(null));
        await command.ExecuteAsync(null);

        Assert.Equal(2, calls);
    }

    // Not inlined: the command and its task must be unreachable once this returns, or the
    // collector cannot finalize the task and an unobserved exception could never be reported.
    private static void PressAndAbandon(Exception thrown, TaskCompletionSource handled)
    {
        var command = new SynchronizedCommand(
            async () =>
            {
                await Task.Yield();
                throw thrown;
            },
            SynchronizationBehavior.Discard,
            true);

        command.Faulted += (_, _) => handled.TrySetResult();
        ((ICommand)command).Execute(null);
    }
}