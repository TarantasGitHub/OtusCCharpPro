// See https://aka.ms/new-console-template for more information
using ConsoleApp;

namespace Consoleapp;
public class Program
{
    public static async Task Main(string[] args)
    {
        int itemCount = 1000000;
        int threadItemsCount = 100000;
        int threadCount = 6;

        if (args.Length > 0)
        {
            switch (args.Length)
            {
                case 3:
                    int.TryParse(args[2], out threadCount);
                    goto case 2;
                case 2:
                    int.TryParse(args[1], out threadItemsCount);
                    goto case 1;
                case 1:
                    int.TryParse(args[0], out itemCount);
                    break;
            }
        }
        // var ints = new List<int>() { 1, 2, 3, 4, 5 };
        var ints = DataGenerator.Generate(itemCount);
        var calculatorCollection = new List<ICalculation>();
        calculatorCollection.Add(new SequentialCalculation());
        calculatorCollection.Add(new PLINQCalculation(threadCount));
        calculatorCollection.Add(new TaskCalculation(threadItemsCount));
        calculatorCollection.Add(new MultiThreadCalculation(threadItemsCount, threadCount));
        calculatorCollection.Add(new ThreadPoolCalculation(threadItemsCount, threadCount));

        Int64 result = 0;
        foreach (var calculator in calculatorCollection)
        {
            result = await calculator.Sum(ints);
            Console.WriteLine("Calculator: {0}, Sum: {1}, Milliseconds: {2}",
                calculator.GetType().Name,
                result,
                calculator.Milliseconds
            );
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        Console.ReadKey();
        //return Task.CompletedTask;
    }
}
