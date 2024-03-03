namespace ConsoleApp;

public static class DataGenerator
{
    public static IEnumerable<int> Generate(int count, int from = 0, int to = 1000)
    {
        List<int> result = new List<int>(capacity: count);

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            result.Add(random.Next(from, to));
        }

        return result;
    }
}
