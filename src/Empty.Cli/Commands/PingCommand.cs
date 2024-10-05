using Empty.Sdk;
using Empty.Sdk.Dtos;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Empty.Cli.Commands;

public sealed class PingCommand : AsyncCommand
{
    private readonly SdkEnvironment _sdkEnv;
    private readonly IFlurlClient _apiClient;
    private readonly IAnsiConsole _console;

    public PingCommand(
        SdkEnvironment sdkEnv,
        [FromKeyedServices(Sdk.HostExtensions.ApiClientKey)] IFlurlClient apiClient,
        IAnsiConsole console)
    {
        _sdkEnv = sdkEnv;
        _apiClient = apiClient;
        _console = console;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var result = await _apiClient
            .Request(_sdkEnv.ApiBaseUrl, "api", "ping", "v1", "time")
            .GetJsonAsync<TimeDto>();        

        _console.MarkupLine($"The server time is [bold]{result.Time}[/]");

        return 0;
    }
}
