using System.Diagnostics;

namespace Namespace;
public class Class
{
    [Fact]
    public async Task RecursionTest1()
    {
        // Arrange
        var r = new Recursion();

        // Act
        //var root = new DirectoryInfo("C:\\Users\\Андрей");
        var root = new DirectoryInfo("C:\\Users");
        var sw = new Stopwatch();
        sw.Start();
        await r.DeleteWithoutRecursion(root);
        
        sw.Stop();
        var firstSw = sw.ElapsedMilliseconds;
        Console.WriteLine("Recursive {0} ms", sw.ElapsedMilliseconds);
        sw.Restart();
        r.RecursiveDelete(root);
        sw.Stop();
        var secondSw = sw.ElapsedMilliseconds;
        Console.WriteLine("Notrecursive {0} ms", sw.ElapsedMilliseconds);

        //Assert
        Assert.Equal(r.FilesWithOutRecursion.Count, r.FilesWithRecursion.Count);
        //Assert.Equal(firstSw, secondSw);
        Assert.Equal(r.FilesWithOutRecursionMilliseconds, r.FilesWithRecursionMilliseconds);
        
    }
}
