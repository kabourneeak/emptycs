namespace Empty.Sdk;

/// <summary>
/// Represents a collection of <see cref="IDisposable"/> objects that are disposed together.
/// </summary>
/// <remarks>
/// This makes it easier to manage the creation of several disposable objects without having to
/// store discrete fields and remembering to dispose of each one individually.
/// </remarks>
public sealed class CompositeDisposable : IDisposable
{
    private readonly IReadOnlyList<IDisposable> _disposables;

    public CompositeDisposable(params IDisposable[] disposables)
    {
        _disposables = disposables;
    }

    public CompositeDisposable(IEnumerable<IDisposable> disposables)
    {
        _disposables = disposables.ToArray();
    }

    public void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}