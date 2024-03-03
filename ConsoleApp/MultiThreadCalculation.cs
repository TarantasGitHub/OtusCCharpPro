using System.Diagnostics;

namespace ConsoleApp;

public class MultiThreadCalculation : BaseCalculation
{
    private readonly int _threadSize;
    private readonly int _threadCounts;

    public MultiThreadCalculation(int threadSize = 100, int threadCounts = 2)
    {
        this._threadSize = threadSize;
        this._threadCounts = threadCounts;
    }

    protected override Task<Int64> CalculateSum(IEnumerable<int> ints)
    {
        var sum = new SumObject { Sum = 0L };
        int i = 0;
        var semaphore = new Semaphore(this._threadCounts, this._threadCounts);
        for (; i < ints.Count(); i += _threadSize)
        {
            var threadContext = new ThreadContext
            {
                Ints = ints,
                From = i,
                To = i + _threadSize > ints.Count() ? ints.Count() : i + _threadSize,
                BaseSum = sum,
                SemaphoreSlim = semaphore
            };
            Thread thread = new Thread(threadContext.ThreadCalculateSum);
            thread.Start();
            thread.Join();
        }
        semaphore.Close();
        semaphore.Dispose();

        return Task.FromResult(sum.Sum);
    }



    private class ThreadContext
    {
        public IEnumerable<int> Ints { get; init; }
        public int From { get; init; }
        public int To { get; init; }
        public SumObject BaseSum { get; init; }
        public Semaphore SemaphoreSlim { get; init; }


        public void ThreadCalculateSum()
        {
            SemaphoreSlim.WaitOne();
            Int64 localSum = Ints.Skip(From).Take(To - From).Sum(x => (Int64)x);
            Interlocked.Add(ref BaseSum.Sum, localSum);
            SemaphoreSlim.Release();
        }
    }

    private class SumObject
    {
        public Int64 Sum;
    }
}
