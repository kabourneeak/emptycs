using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Empty.Cli;

public class Program
{
    /// <summary>
    /// The entry point for the application.
    /// </summary>
    /// <param name="args">Args from the shell or process that starts this application</param>
    /// <returns>The exit code of the application, <c>0</c> for success.</returns>
    public static Task<int> Main(string[] args)
    {
        return MainInternal(args);
    }

    internal static Task<int> MainInternal(
        string[] args,
        Action<IServiceCollection>? serviceOverrides = default)
    {
        var host = CreateDefaultBuilder(args, serviceOverrides)
            .Build();

        return host.RunCliAsync(args);
    }

    internal static IHostBuilder CreateDefaultBuilder(
        string[]? args = default,
        Action<IServiceCollection>? serviceOverrides = default)
    {
        // configure default host, in order to get host goodies
        // like configuration, logging, and DI
        var builder = Host.CreateDefaultBuilder(args ?? []);

        // configure logging
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "[HH:mm:ss] ";
            });
            logging.SetMinimumLevel(LogLevel.Warning);
        });

        // add this project's services
        builder.AddCli();

        // apply service overrides from test environment
        builder.ConfigureServices(services =>
        {
            serviceOverrides?.Invoke(services);
        });

        return builder;
    }
}
