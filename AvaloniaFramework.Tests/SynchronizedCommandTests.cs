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
}