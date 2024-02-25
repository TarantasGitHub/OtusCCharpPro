namespace ConsoleApp;

public class ParallelCalculation: BaseCalculation
{
    protected override Int64 CalculateSum(IEnumerable<int> ints)
    {
        return ints.AsParallel().Sum(i => (Int64)i);
    }
}
