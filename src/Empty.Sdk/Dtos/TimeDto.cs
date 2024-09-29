namespace Empty.Sdk.Dtos;

/// <summary>
/// Represents a time value.
/// </summary>
public record TimeDto
{
    /// <summary>
    /// The time value.
    /// </summary>
    public DateTimeOffset Time { get; set; }
}