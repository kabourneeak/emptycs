namespace Empty.Sdk.Models;

/// <summary>
/// Thrown when a request times out.
/// </summary>
public class TimedOutException : SdkException
{
    public TimedOutException(string message) : base(message)
    {
    }

    public TimedOutException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
