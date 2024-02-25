namespace ConsoleApp;

public class TaskCalculation : BaseCalculation
{
    private readonly int _pageSize;
    public TaskCalculation(int pageSize = 100)
    {
        this._pageSize = pageSize;
    }
    protected override Int64 CalculateSum(IEnumerable<int> ints)
    {
        //var tail = ints.Count() % this._pageSize;
        List<Task<Int64>> tasks = new List<Task<Int64>>();

        int i = 0;
        int[] taskInts = new int[_pageSize];
        foreach (var item in ints)
        {
            taskInts[i++] = item;
            if (i == _pageSize)
            {
                tasks.Add(Task.Run<Int64>(() => PartCalculation(taskInts)));
                taskInts = new int[_pageSize];
                i = 0;
            }
        }
        if (i > 0)
        {
            tasks.Add(Task.Run<Int64>(() => PartCalculation(taskInts)));
        }
        Task.WhenAll(tasks);
        return tasks.Select(t => t.Result).Sum();
    }

    private Int64 PartCalculation(IEnumerable<int> ints)
    {
        return ints.Sum(x => (Int64)x);
    }
}
