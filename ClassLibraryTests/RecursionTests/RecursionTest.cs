using System.Diagnostics;

namespace Namespace;
public class Class
{
    [Fact]
    public void RecursionTest1()
    {
        // Arrange
        var r = new Recursion();

        // Act
        var root = new DirectoryInfo("/home");
        var sw = new Stopwatch();
        sw.Start();
        r.DeleteWithoutRecursion(root);
        
        sw.Stop();
        var firstSw = sw.ElapsedMilliseconds;
        Console.WriteLine("Recursive {0} ms", sw.ElapsedMilliseconds);
        sw.Restart();
        r.RecursiveDelete(root);
        sw.Stop();
        var secondSw = sw.ElapsedMilliseconds;
        Console.WriteLine("Notrecursive {0} ms", sw.ElapsedMilliseconds);

        //Assert
        Assert.Equivalent(r.FilesWithOutRecursion, r.FilesWithRecursion);
        //Assert.Equal(firstSw, secondSw);
        Assert.Equal(r.FilesWithOutRecursionMilliseconds, r.FilesWithRecursionMilliseconds);
        
    }
}
