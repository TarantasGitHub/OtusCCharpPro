namespace ConsoleApp
{
    internal class SequentialCalculationLINQ : BaseCalculation
    {
        protected override Task<Int64> CalculateSum(IEnumerable<int> ints)
        {
            return Task.FromResult(ints.Sum(i => (Int64)i));
        }
    }
}
