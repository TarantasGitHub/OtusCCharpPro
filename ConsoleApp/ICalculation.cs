namespace ConsoleApp;

public interface ICalculation
{
    Int64 Milliseconds { get; }
    Int64 Sum(IEnumerable<int> ints);
}
