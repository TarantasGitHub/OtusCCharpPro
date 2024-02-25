using System.Diagnostics;

namespace ConsoleApp;

public abstract class BaseCalculation : ICalculation
{
    public Int64 Milliseconds { get; protected set; }

    public Int64 Sum(IEnumerable<int> ints)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Int64 result = CalculateSum(ints);
        stopwatch.Stop();
        this.Milliseconds = stopwatch.ElapsedMilliseconds;
        return result;
    }

    protected abstract Int64 CalculateSum(IEnumerable<int> ints);
}
