namespace Empty.Tests;

public static class AssertExtensions
{
    /// <summary>
    /// Asserts that the value is of the expected type, and returns that value for chaining.
    /// This method is useful for reducing generic returns and stripping nullability while
    /// asserting expected values.
    /// </summary>
    /// <typeparam name="T">The expected type</typeparam>
    /// <param name="value">The object to test</param>
    /// <returns>The object cast as <typeparamref name="T"/></returns>
    /// <exception cref="AssertionException">If <paramref name="value"/> is not assignable to <typeparamref name="T"/>.</exception>
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