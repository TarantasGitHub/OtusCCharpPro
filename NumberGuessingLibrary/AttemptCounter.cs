namespace NumberGuessingLibrary;

internal class AttemptCounter
{
    public int MaxAttemptCount { get { return _maxAttemptCount; } }
    public int CurrentAttemptCount { get { return _currentAttemptCount; } }
    private readonly int _maxAttemptCount;
    private int _currentAttemptCount;
    public AttemptCounter(int maxAttemptCount)
    {
        this._maxAttemptCount = maxAttemptCount;
        this._currentAttemptCount = 0;
    }

    public void Increment()
    {
        this._currentAttemptCount++;
    }

    public bool GameOver()
    {
        return this._currentAttemptCount >= this._maxAttemptCount;
    }
}
