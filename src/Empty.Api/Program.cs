using Prometheus;

namespace Empty.Api;

internal class Program
{
    /// <summary>
    /// The entry point for the application.
    /// </summary>
    /// <param name="args">Arguments coming in from the operating system.</param>
    public static void Main(string[] args)
    {
        var app = MainInternal(args);

        app.Run();
    }

    internal static WebApplication MainInternal(        
        string[]? args = default,
        Action<IServiceCollection>? serviceOverrides = default)
    {
        var builder = CreateDefaultBuilder(args, serviceOverrides);
        var app = CreateDefaultApp(builder);

        return app;
    }

    internal static WebApplicationBuilder CreateDefaultBuilder(
        string[]? args = default,
        Action<IServiceCollection>? serviceOverrides = default)
    {
        var builder = WebApplication.CreateBuilder(args ?? []);

        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
                options.TimestampFormat = "[HH:mm:ss] ";
            });
        });

        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers()
            // add the controllers from this project, even when loaded by the test project
            .AddApplicationPart(typeof(Program).Assembly)
            .AddControllersAsServices();

        // to add our services from <see cref="HostExtensions"/>
        builder.AddApi();

        // apply service overrides
        serviceOverrides?.Invoke(builder.Services);

        return builder;
    }

    internal static WebApplication CreateDefaultApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c => c
            .EnableTryItOutByDefault());
        app.UseMetricServer();

        app.MapControllers();

        return app;
    }
}
