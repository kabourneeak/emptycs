using Empty.Cli.Commands;
using Empty.Sdk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace Empty.Cli;

public static class HostExtensions
{
    /// <summary>
    /// Add the CLI to the service collection.
    /// </summary>
    /// <param name="hostBuilder">The service collection to extend.</param>
    /// <returns><paramref name="hostBuilder"/>, for chaining.</returns>
    public static IHostBuilder AddCli(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddSdk();
            services.AddCommandApp();
        });

        return hostBuilder;
    }

    private static IServiceCollection AddCommandApp(this IServiceCollection services)
    {
        var typeRegistrar = new TypeRegistrar(services);

        var app = new CommandApp(typeRegistrar);

        app.Configure(config =>
        {
            config.AddCommand<PingCommand>("ping")
                .WithDescription("Ping the server.");
        });

        services.AddSingleton<ITypeRegistrar>(typeRegistrar);
        services.AddSingleton<ICommandApp>(app);

        return services;
    }

    public static async Task<int> RunCliAsync(this IHost host, string[] args)
    {
        var cmdApp = host.Services.GetRequiredService<ICommandApp>();

        await host.StartAsync();

        try
        {
            return await cmdApp.RunAsync(args);
        }
        finally
        {
            await host.StopAsync();
        }
    }
}