namespace Empty.Tests;

public static class AssertExtensions
{
    public static T AssertType<T>(this object? value)
        where T : class
    {
        if (value is T result)
        {
            return result;
        }

        throw new AssertionException($"Expected {typeof(T).Name}, but found {value?.GetType().Name ?? "null"}.");
    }
}