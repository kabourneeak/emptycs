namespace Empty.Sdk.Models;

/// <summary>
/// Thrown when a resource is not found.
/// </summary>
public class NotFoundException : SdkException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
