using Empty.Sdk.Endpoints;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Empty.Sdk;

public static class HostExtensions
{
    /// <summary>
    /// The service provider key for the API <see cref="IFlurlClient"/>.
    /// </summary>
    public const string ApiClientKey = nameof(ApiClientKey);

    /// <summary>
    /// Add the SDK to the service collection
    /// </summary>
    /// <param name="services">The service collection to extend.</param>
    /// <returns><paramref name="services"/>, for chaining.</returns>
    public static IServiceCollection AddSdk(this IServiceCollection services)
    {
        services.AddSingleton<SdkEnvironment>();

        services.AddSingleton<IFlurlClientCache>(c => new FlurlClientCache()
            .Add(ApiClientKey));

        services.AddKeyedSingleton<IFlurlClient>(ApiClientKey, (c, _) =>
            c.GetRequiredService<IFlurlClientCache>().Get(ApiClientKey));

        services.AddSingleton<PingV1Endpoints>();

        return services;
    }
}
