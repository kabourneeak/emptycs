using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Time.Testing;
using ApiProgram = Empty.Api.Program;

namespace Empty.Tests;

public sealed class TestApiServer : IDisposable, IAsyncDisposable
{
    private readonly IHost _host;

    public TestApiServer(
        FakeTimeProvider timeProvider,
        Action<IServiceCollection>? serviceOverrides = default)
    {
        var defaultServiceOverrides = new Action<IServiceCollection>(services =>
        {
            services.AddSingleton<TimeProvider>(timeProvider);
        });

        _host = ApiProgram.MainInternal(serviceOverrides: defaultServiceOverrides + serviceOverrides);
    }

    /// <summary>
    /// Access to the service collection provided by the API Server.
    /// </summary>
    public IServiceProvider Services => _host.Services;

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
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        _host.Dispose();

        return ValueTask.CompletedTask;
    }
}