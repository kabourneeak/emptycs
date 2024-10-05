using Empty.Sdk;
using Flurl;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Time.Testing;
using ApiProgram = Empty.Api.Program;

namespace Empty.Tests;

/// <summary>
/// A test harness for standing up an instance of the API Server for testing.
/// This class expects to be created and managed by <see cref="TestEnvironment"/>.
/// </summary>
public sealed class TestApiServer : IDisposable, IAsyncDisposable
{
    private readonly CompositeDisposable _envVars;

    private readonly IHost _host;

    public TestApiServer(
        FakeTimeProvider timeProvider,
        Action<IServiceCollection>? apiServiceOverrides = default)
    {
        var defaultServiceOverrides = new Action<IServiceCollection>(services =>
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

        _envVars = new CompositeDisposable(
            new DisposableEnvironmentVariable("ASPNETCORE_URLS", BaseUrl));

        _host = ApiProgram.MainInternal(serviceOverrides: defaultServiceOverrides + apiServiceOverrides);
    }

    /// <summary>
    /// Access to the service collection provided by the API Server.
    /// </summary>
    public IServiceProvider Services => _host.Services;

    /// <summary>
    /// The base URL of the Test API Server.
    /// </summary>
    public Url BaseUrl => new("http://localhost:5197");

    /// <summary>
    /// Start the API Server. Notably, this starts running all background services.
    /// </summary>
    public Task StartAsync()
    {
        return _host.StartAsync();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _host.Dispose();
        _envVars.Dispose();
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        // call the synchronous dispose method
        Dispose();

        return ValueTask.CompletedTask;
    }
}
