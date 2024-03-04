namespace ConsoleApp
{
    internal class ThreadPoolCalculation : BaseCalculation
    {
        private readonly int _threadSize;
        private readonly int _threadCounts;

        public ThreadPoolCalculation(int threadSize = 100, int threadCounts = 2)
        {
            this._threadSize = threadSize;
            this._threadCounts = threadCounts;
        }

        protected override Task<Int64> CalculateSum(IEnumerable<int> ints)
        {
            var sum = new SumObject { Sum = 0L };
            
            using (var semaphore = new Semaphore(this._threadCounts, this._threadCounts))
            {
                using (var countdownEvent = new CountdownEvent(1))
                {
                    for (int i = 0; i < ints.Count(); i += _threadSize)
                    {
                        var threadContext = new ThreadContext
                        {
                            Ints = ints,
                            From = i,
                            To = i + _threadSize > ints.Count() ? ints.Count() : i + _threadSize,
                            BaseSum = sum,
                            Sem = semaphore,
                        };
                        countdownEvent.AddCount();
                        ThreadPool.QueueUserWorkItem(delegate (object context)
                        {                            
                            try { ThreadPoolCalculateSum(context); }
                            finally { countdownEvent.Signal(); }
                        }, threadContext);
                    }
                    countdownEvent.Signal();
                    countdownEvent.Wait();
                }
            }
            //Thread.Sleep(1000);
            return Task.FromResult(sum.Sum);
        }

        private void ThreadPoolCalculateSum(Object threadContext)
        {
            if (threadContext is ThreadContext tc)
            {
                tc.Sem.WaitOne();
                Int64 localSum = tc.Ints.Skip(tc.From).Take(tc.To - tc.From).Sum(x => (Int64)x);
                Interlocked.Add(ref tc.BaseSum.Sum, localSum);
                tc.Sem.Release();
            }
        }

        private class ThreadContext
        {
            public IEnumerable<int> Ints { get; init; }
            public int From { get; init; }
            public int To { get; init; }
            public SumObject BaseSum { get; init; }
            public Semaphore Sem { get; init; }
        }

        private class SumObject
        {
            public Int64 Sum;
        }
    }
}