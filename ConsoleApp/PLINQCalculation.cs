namespace ConsoleApp;

public class PLINQCalculation : BaseCalculation
{
    private readonly int _degreeOfParallelism;

    public PLINQCalculation(int degreeOfParallelism)
    {
        this._degreeOfParallelism = degreeOfParallelism;
    }

    protected override Task<Int64> CalculateSum(IEnumerable<int> ints)
    {
        return Task.FromResult(ints.AsParallel().WithDegreeOfParallelism(this._degreeOfParallelism).Sum(i => (Int64)i));
    }
}
