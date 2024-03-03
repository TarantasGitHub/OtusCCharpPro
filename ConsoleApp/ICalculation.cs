namespace ConsoleApp;

public interface ICalculation
{
    Int64 Milliseconds { get; }
    Task<Int64> Sum(IEnumerable<int> ints);
}
