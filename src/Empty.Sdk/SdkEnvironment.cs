using Flurl;
using Microsoft.Extensions.Hosting;

namespace Empty.Sdk;

/// <summary>
/// Environmental configuration for the SDK.
/// </summary>
public class SdkEnvironment
{
    private readonly IHostEnvironment _hostEnv;
    private readonly Lazy<string> _apiBaseUrl;

    /// <summary>
    /// The prefix for environment variables used by the SDK.
    /// </summary>
    public const string EnvVarPrefix = "EMPTY_";

    /// <summary>
    /// The environment variable name for the API base URL.
    /// </summary>
    public const string ApiBaseUrlEnvVar = EnvVarPrefix + "API_BASE_URL";

    public SdkEnvironment(IHostEnvironment hostEnv)
    {
        _hostEnv = hostEnv;
        _apiBaseUrl = new Lazy<string>(GetBaseUrl);
    }

    /// <summary>
    /// The base URL of the API, as configured in the environment.
    /// This value always uses the URL from <see cref="ApiBaseUrlEnvVar"/>, if given,
    /// otherwise it tries to find a default value based on the current environment.
    /// </summary>
    /// <returns>A new instance of <see cref="Url"/> representing the API base URL.</returns>
    /// <exception cref="SdkException">Thrown when the API base URL is not configured.</exception>
    public Url ApiBaseUrl => new(_apiBaseUrl.Value);

    private string GetBaseUrl()
    {
        var baseUrl = Environment.GetEnvironmentVariable(ApiBaseUrlEnvVar);

        if (baseUrl is not null)
        {
            return baseUrl;
        }

        if (_hostEnv.IsDevelopment())
        {
            return "http://localhost:5197";
        }

        throw new SdkException($"The API base URL is not configured. Set the environment variable '{ApiBaseUrlEnvVar}'.");
    }
}
