using Empty.Cli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Time.Testing;
using Spectre.Console.Cli;
using Spectre.Console.Testing;
using CliProgram = Empty.Cli.Program;

namespace Empty.Tests;

/// <summary>
/// A test harness for standing up an instance of the CLI for testing.
/// This class expects to be created and managed by <see cref="TestEnvironment"/>.
/// </summary>
public sealed class TestCli : IDisposable
{
    private readonly Action<IServiceCollection> _defaultServiceOverrides;

    private readonly Action<IServiceCollection>? _cliServiceOverrides;

    public TestCli(
        FakeTimeProvider timeProvider,
        Action<IServiceCollection>? cliServiceOverrides = default)
    {
        _defaultServiceOverrides = new Action<IServiceCollection>(services =>
        {
            // bring in the time provider from the test environment
            services.AddSingleton<TimeProvider>(timeProvider);

            // replace logging with a debug logger to hide output unless a test is
            // explicitly being debugged.
            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddDebug();
            });
        });

        _cliServiceOverrides = cliServiceOverrides;
    }

    /// <summary>
    /// Run the CLI program with the given arguments.
    /// </summary>
    /// <param name="args">The arguments to pass to the Cli</param>
    /// <returns>The result of the Cli run</returns>
    /// <remarks>
    /// Inspired by https://github.com/spectreconsole/spectre.console/blob/main/src/Spectre.Console.Testing/Cli/CommandAppTester.cs
    /// </remarks>
    public async Task<TestCliRunResult> RunAsync(string[] args)
    {
        // arrange the service container instance for this run
        using var host = CliProgram.CreateDefaultBuilder(args, _defaultServiceOverrides + _cliServiceOverrides)
            .Build();

        // grab the command from the host
        var cmdApp = host.Services.GetRequiredService<ICommandApp>();

        // wire up the test console
        using var console = new TestConsole().Width(int.MaxValue);

        cmdApp.Configure(c => c.ConfigureConsole(console));

        // wire up the command interceptor
        var context = default(CommandContext?);
        var settings = default(CommandSettings?);

        cmdApp.Configure(c => c.SetInterceptor(new CallbackCommandInterceptor((ctx, s) =>
        {
            context = ctx;
            settings = s;
        })));

        // run the CLI program
        var exitCode = await host.RunCliAsync(args);

        var output = console.Output
            .NormalizeLineEndings()
            .TrimLines()
            .Trim();

        return new TestCliRunResult(exitCode, output, context, settings);
    }

    public void Dispose()
    {
    }
}
