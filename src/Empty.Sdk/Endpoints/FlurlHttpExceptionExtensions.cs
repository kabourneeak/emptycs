using Empty.Sdk.Models;
using Flurl.Http;

namespace Empty.Sdk.Endpoints;

/// <summary>
/// Extension methods for <see cref="FlurlHttpException"/>.
/// </summary>
public static class FlurlHttpExceptionExtensions
{
    /// <summary>
    /// Creates an <see cref="SdkException"/> from a <see cref="FlurlHttpException"/>.
    /// </summary>
    /// <param name="ex">The flurl exception to convert.</param>
    /// <returns>A new instance of <see cref="SdkException"/> or one of its derived types.</returns>
    public static SdkException ToSdkException(this FlurlHttpException ex)
    {
        return ex switch
        {
            FlurlHttpTimeoutException => new TimedOutException(ex.Message),
            { Call.Response.StatusCode: 404 } => new NotFoundException(ex.Message, ex),
            _ => new SdkException($"Call returned {ex.Call.Response.StatusCode}: {ex.Call.Request.Verb} {ex.Call.Request.Url}", ex)
        };
    }
}
