using Empty.Sdk;
using Flurl.Http.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Time.Testing;

namespace Empty.Tests;

/// <summary>
/// Represents a test environment that integrates each part of the solution.
/// </summary>
public sealed class TestEnvironment : IDisposable, IAsyncDisposable
{
    private readonly CompositeDisposable _envVars;

    private readonly TestApiServer? _apiServer;

    public TestEnvironment(
        bool useApiServer,
        Action<IServiceCollection>? apiServiceOverrides = default)
    {
        // set the environment variables
        _envVars = new CompositeDisposable(
            new DisposableEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environments.Development));

        // set the http edge
        Http = new HttpTest();

        // set the time edge
        Time = new FakeTimeProvider();
        Time.SetUtcNow(new DateTime(2024, 1, 1));

        if (useApiServer)
        {
            _apiServer = new TestApiServer(
                Time,
                apiServiceOverrides);

            // allow real HTTP calls to the Test API server
            Http.ForCallsTo(_apiServer.BaseUrl.AppendPathSegment("*"))
                .AllowRealHttp();
        }
    }

    /// <summary>
    /// Access for HTTP edge for this environment.
    /// </summary>
    public HttpTest Http { get; }

    /// <summary>
    /// Access for time edge for this environment.
    /// </summary>
    public FakeTimeProvider Time { get; }

    /// <summary>
    /// Access for API Server for this environment, if configured.
    /// </summary>
    public TestApiServer ApiServer => _apiServer
        ?? throw new InvalidOperationException("API Server is not enabled in this environment.");

    /// <summary>
    /// Start the API Server, if configured, along with other long-running
    /// elements of the environment.
    /// </summary>
    /// <remarks>
    /// In many cases, you can test without actually starting all of the background
    /// services of the environment, which may result in faster tests.
    /// </remarks>
    public async Task StartAsync()
    {
        if (_apiServer is not null)
        {
            await _apiServer.StartAsync();
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        _apiServer?.Dispose();
        _envVars.Dispose();
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        if (_apiServer is not null)
        {
            await _apiServer.DisposeAsync();
        }

        _envVars.Dispose();
    }
}
