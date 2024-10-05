using Spectre.Console.Cli;

namespace Empty.Tests;

/// <summary>
/// Represents the result of a completed <see cref="CommandApp"/> run.
/// </summary>
public sealed class TestCliRunResult
{
    /// <summary>
    /// Gets the exit code.
    /// </summary>
    public int ExitCode { get; }

    /// <summary>
    /// Gets the console output.
    /// </summary>
    public string Output { get; }

    /// <summary>
    /// Gets the command context.
    /// </summary>
    public CommandContext? Context { get; }

    /// <summary>
    /// Gets the command settings.
    /// </summary>
    public CommandSettings? Settings { get; }

    internal TestCliRunResult(int exitCode, string output, CommandContext? context, CommandSettings? settings)
    {
        ExitCode = exitCode;
        Output = output;
        Context = context;
        Settings = settings;
    }
}