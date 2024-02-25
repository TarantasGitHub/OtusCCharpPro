namespace ConsoleApp;

public class SequentialCalculation : BaseCalculation
{
    protected override Int64 CalculateSum(IEnumerable<int> ints)
    {
        return ints.Sum(i => (Int64)i);
    }
}
