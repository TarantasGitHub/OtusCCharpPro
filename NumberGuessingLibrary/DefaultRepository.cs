namespace NumberGuessingLibrary;

internal class DefaultRepository : IRepository
{
    public int MaxAttemptCount() => 10;
    public int From() => 0;
    public int To() => 100;
}
