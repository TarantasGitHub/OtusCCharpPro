namespace NumberGuessingLibrary;

internal class AttemptValueChecker
{
    private readonly int _askingValue;
    public AttemptValueChecker(int askingValue)
    {
        this._askingValue = askingValue;
    }

    public AttemptStatus Check(int userValue)
    {
        if (userValue == _askingValue)
        {
            return AttemptStatus.Equal;
        }
        else if (userValue > _askingValue)
        {
            return AttemptStatus.More;
        }
        else
        {
            return AttemptStatus.Less;
        }
    }
}
