// See https://aka.ms/new-console-template for more information
using ConsoleApp;

namespace Consoleapp;
public class Program
{
    public static async Task Main()
    {
        // var ints = new List<int>() { 1, 2, 3, 4, 5 };
        var ints = DataGenerator.Generate(1000000);
        var calculatorCollection = new List<ICalculation>();
        calculatorCollection.Add(new SequentialCalculation());
        calculatorCollection.Add(new ParallelCalculation());
        calculatorCollection.Add(new TaskCalculation(100000));
        calculatorCollection.Add(new MultiThreadCalculation(100000, 50));

        Int64 result = 0;
        foreach (var calculator in calculatorCollection)
        {
            result = await calculator.Sum(ints);
            Console.WriteLine("Calculator: {0}, Sum: {1}, Milliseconds: {2}",
                calculator.GetType().Name,
                result,
                calculator.Milliseconds
            );
        }
        Console.ReadKey();
        //return Task.CompletedTask;
    }
}
