namespace ConsoleApp;

public class ParallelCalculation: BaseCalculation
{
    protected override Task<Int64> CalculateSum(IEnumerable<int> ints)
    {
        return Task.FromResult(ints.AsParallel().Sum(i => (Int64)i));
    }
}
