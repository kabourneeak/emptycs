using Microsoft.Extensions.DependencyInjection;

namespace Empty.Tests;

public class TestEnvironmentBuilder
{
    bool _useApiServer;

    Action<IServiceCollection>? _apiServiceOverrides;

    public TestEnvironmentBuilder WithApiServer(
        Action<IServiceCollection>? apiServiceOverrides = default)
    {
        _useApiServer = true;
        _apiServiceOverrides = apiServiceOverrides;

        return this;
    }

    public TestEnvironment Build()
    {
        return new TestEnvironment(
            _useApiServer,
            _apiServiceOverrides);
    }
}
