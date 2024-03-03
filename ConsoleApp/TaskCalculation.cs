namespace ConsoleApp;

public class TaskCalculation : BaseCalculation
{
    private readonly int _pageSize;
    public TaskCalculation(int pageSize = 100)
    {
        this._pageSize = pageSize;
    }
    
    protected override async Task<Int64> CalculateSum(IEnumerable<int> ints)
    {
        var intsArray = ints.ToArray();
        List<Task<Int64>> tasks = new List<Task<Int64>>();
        int pageCount = intsArray.Length / _pageSize;
        for (int j = 0; j < pageCount; j++)
        {
            int[] tmpInts = new int[_pageSize];
            Array.Copy(intsArray, j * _pageSize, tmpInts, 0, _pageSize);
            tasks.Add(Task.Run<Int64>(() => PartCalculation(tmpInts)));
        }
        var tail = intsArray.Length % _pageSize;
        if (tail > 0)
        {
            int[] tmpInts = new int[tail];
            Array.Copy(intsArray, pageCount * _pageSize, tmpInts, 0, tail);
            tasks.Add(Task.Run<Int64>(() => PartCalculation(tmpInts)));
        }
        await Task.WhenAll(tasks);
        return tasks.Select(t => t.Result).Sum();
    }    

    private Int64 PartCalculation(IEnumerable<int> ints)
    {
        return ints.Sum(x => (Int64)x);
    }
}
