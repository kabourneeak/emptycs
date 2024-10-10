using Empty.Sdk.Endpoints;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Empty.Cli.Commands;

public sealed class PingCommand : AsyncCommand
{
    private readonly PingV1Endpoints _pingV1;
    private readonly IAnsiConsole _console;

    public PingCommand(
        PingV1Endpoints pingV1,
        IAnsiConsole console)
    {
        _pingV1 = pingV1;
        _console = console;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var result = await _pingV1.Time(CancellationToken.None);

        _console.MarkupLine($"The server time is [bold]{result.Time}[/]");

        return 0;
    }
}
