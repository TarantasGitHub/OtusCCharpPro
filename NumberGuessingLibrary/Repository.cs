namespace NumberGuessingLibrary;

public class Repository : IRepository
{
    private readonly int _maxAttemptCount;
    private readonly int _from;
    private readonly int _to;
    public Repository(int maxAttemptCount, int from, int to)
    {
        this._maxAttemptCount = maxAttemptCount;
        this._from = from;
        this._to = to;
    }

    public int MaxAttemptCount() => this._maxAttemptCount;
    public int From() => this._from;
    public int To() => this._to;
}
