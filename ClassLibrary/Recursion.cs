using System.Diagnostics;

namespace Namespace;
public class Recursion
{

    public List<string> FilesWithRecursion { get; private set; }
    public long FilesWithRecursionMilliseconds { get; private set; }
    public List<string> FilesWithOutRecursion { get; private set; }
    public long FilesWithOutRecursionMilliseconds { get; private set; }

    public Recursion()
    {
        FilesWithRecursion = new List<string>();
        FilesWithOutRecursion = new List<string>();
    }

    public void RecursiveDelete(DirectoryInfo baseDir, bool isRoot = true)
    {
        var options = new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = false,

        };

        var sw = new Stopwatch();
        if (isRoot)
        {
            sw.Start();
        }

        if (!baseDir.Exists)
            return;

        foreach (var dir in baseDir.EnumerateDirectories("*", options))
        {
            RecursiveDelete(dir, false);
        }
        var files = baseDir.GetFiles("*", options);
        foreach (var file in files)
        {
            try
            {
                file.IsReadOnly = false;
                DoSomeThingWithFile(string.Format("\t{0}", file.FullName), true);
                // file.Delete();
            }
            catch { }
        }
        DoSomeThingWithFile(baseDir.FullName, true);
        // baseDir.Delete();
        if (isRoot)
        {
            sw.Stop();
            FilesWithRecursionMilliseconds = sw.ElapsedMilliseconds;
        }
    }

    public void DeleteWithoutRecursion(DirectoryInfo baseDir)
    {
        var options = new EnumerationOptions
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = false,

        };

        var sw = new Stopwatch();
        sw.Start();

        if (!baseDir.Exists)
            return;

        bool hasChild;
        List<string> currentLayer;
        var folderTreeStack = new Stack<string[]>();

        folderTreeStack.Push(new[] { baseDir.FullName });

        do
        {
            currentLayer = new List<string>();
            hasChild = false;
            foreach (var path in folderTreeStack.Peek())
            {
                var di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    currentLayer.AddRange(
                        di//.GetDirectories("*.*", options)
                        .EnumerateDirectories("*", options)
                        //.Where(di => di.FullName != "/proc/5717/cwd")
                        .Select(d => d.FullName));
                    if (!hasChild && di.EnumerateDirectories("*", options).Any())
                    {
                        hasChild = true;
                    }
                }
            }
            folderTreeStack.Push(currentLayer.ToArray());
        } while (hasChild);

        while (folderTreeStack.Count > 0)
        {
            foreach (var path in folderTreeStack.Pop())
            {
                var di = new DirectoryInfo(path);
                if (di.Exists)
                {
                    var files = di.GetFiles("*", options);
                    foreach (var file in files)
                    {
                        try
                        {
                            file.IsReadOnly = false;
                            DoSomeThingWithFile(string.Format("\t{0}", file.FullName), false);
                            // file.Delete();
                        }
                        catch { }
                    }
                    DoSomeThingWithFile(di.FullName, false);
                    // di.Delete();
                }
            }
        }
        sw.Stop();
        FilesWithOutRecursionMilliseconds = sw.ElapsedMilliseconds;
    }

    private void DoSomeThingWithFile(string text, bool recursion)
    {
        if (recursion)
        {
            FilesWithRecursion.Add(text);
        }
        else
        {
            FilesWithOutRecursion.Add(text);
        }
    }
}
