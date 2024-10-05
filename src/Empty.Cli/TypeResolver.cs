using Spectre.Console.Cli;

namespace Empty.Cli;

/// <summary>
/// A type resolver that uses the Microsoft.Extensions.DependencyInjection library.
/// Taken from https://github.com/spectreconsole/examples/blob/main/examples/Cli/Injection/Infrastructure/TypeResolver.cs
/// </summary>
public sealed class TypeResolver : ITypeResolver, IDisposable
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public object? Resolve(Type? type)
    {
        if (type is null)
        {
            return null;
        }

        return _provider.GetService(type);
    }

    public void Dispose()
    {
        if (_provider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}