using Empty.Sdk.Models;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Empty.Sdk.Endpoints;

public class PingV1Endpoints
{
    private readonly SdkEnvironment _sdkEnv;
    private readonly IFlurlClient _apiClient;

    public PingV1Endpoints(
        SdkEnvironment sdkEnv,
        [FromKeyedServices(HostExtensions.ApiClientKey)] IFlurlClient apiClient
    )
    {
        _sdkEnv = sdkEnv;
        _apiClient = apiClient;
    }

    public async Task<TimeDto> Time(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _apiClient
                .Request(_sdkEnv.ApiBaseUrl, "api", "ping", "v1", "time")
                .GetJsonAsync<TimeDto>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        catch (FlurlHttpException ex)
        {
            throw ex.ToSdkException();
        }
    }
}
