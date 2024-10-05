using Microsoft.Extensions.DependencyInjection;

namespace Empty.Tests;

public class TestEnvironmentBuilder
{
    private bool _useApiServer;

    private Action<IServiceCollection>? _apiServiceOverrides;

    private bool _useCli;

    private Action<IServiceCollection>? _cliServiceOverrides;

    public TestEnvironmentBuilder WithApiServer(
        Action<IServiceCollection>? apiServiceOverrides = default
    )
    {
        _useApiServer = true;
        _apiServiceOverrides = apiServiceOverrides;

        return this;
    }

    public TestEnvironmentBuilder WithCli(
        Action<IServiceCollection>? cliServiceOverrides = default
    )
    {
        _useCli = true;
        _cliServiceOverrides = cliServiceOverrides;

        return this;
    }

    public TestEnvironment Build()
    {
        return new TestEnvironment(
            _useApiServer,
            _useCli,
            _apiServiceOverrides,
            _cliServiceOverrides);
    }
}
