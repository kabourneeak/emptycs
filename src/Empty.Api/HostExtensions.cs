using Microsoft.Extensions.DependencyInjection.Extensions;
using Empty.Sdk;

namespace Empty.Api;

internal static class HostExtensions
{
    /// <summary>
    /// Add the API Server project to the host builder
    /// </summary>
    /// <param name="builder">The host builder to update.</param>
    /// <returns><paramref name="builder"/>, to enable chaining.</returns>
    public static IHostApplicationBuilder AddApi(this IHostApplicationBuilder builder)
    {
        builder.Services.TryAddSingleton(TimeProvider.System);

        builder.Services.AddSdk();

        return builder;
    }
}
