namespace ConsoleApp;

public class MultiThreadCalculationLINQ : BaseCalculation
{
    private readonly int _threadSize;
    private readonly int _threadCounts;

    public MultiThreadCalculationLINQ(int threadSize = 100, int threadCounts = 2)
    {
        this._threadSize = threadSize;
        this._threadCounts = threadCounts;
    }

    protected override Task<Int64> CalculateSum(IEnumerable<int> ints)
    {
        var sum = new SumObject { Sum = 0L };
        
        using (var semaphore = new Semaphore(this._threadCounts, this._threadCounts))
        {
            for (int i = 0; i < ints.Count(); i += _threadSize)
            {
                var threadContext = new ThreadContext
                {
                    Ints = ints,
                    From = i,
                    To = i + _threadSize > ints.Count() ? ints.Count() : i + _threadSize,
                    BaseSum = sum,
                    Sem = semaphore
                };
                Thread thread = new Thread(threadContext.ThreadCalculateSum);
                thread.Start();
                thread.Join();
            }
        }

        return Task.FromResult(sum.Sum);
    }

    private class ThreadContext
    {
        public IEnumerable<int> Ints { get; init; }
        public int From { get; init; }
        public int To { get; init; }
        public SumObject BaseSum { get; init; }
        public Semaphore Sem { get; init; }


        public void ThreadCalculateSum()
        {
            Sem.WaitOne();
            Int64 localSum = Ints.Skip(From).Take(To - From).Sum(x => (Int64)x);
            Interlocked.Add(ref BaseSum.Sum, localSum);
            Sem.Release();
        }
    }

    private class SumObject
    {
        public Int64 Sum;
    }
}
