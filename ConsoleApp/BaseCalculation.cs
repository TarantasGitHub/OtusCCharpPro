using System.Diagnostics;

namespace ConsoleApp;

public abstract class BaseCalculation : ICalculation
{
    public Int64 Milliseconds { get; protected set; }

    public async Task<Int64> Sum(IEnumerable<int> ints)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Int64 result = await CalculateSum(ints);
        stopwatch.Stop();
        this.Milliseconds = stopwatch.ElapsedMilliseconds;
        return result;
    }

    protected abstract Task<Int64> CalculateSum(IEnumerable<int> ints);
}
