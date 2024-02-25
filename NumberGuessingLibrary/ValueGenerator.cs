namespace NumberGuessingLibrary;

internal static class ValueGenerator
{
    public static int GetValue(int from, int to)
    {
        if (from >= 0 && from <= to)
        {
            var rnd = new Random();
            return rnd.Next(from, to);
        }
        throw new ArgumentException("Неправильно заданы аргументы");
    }
}
