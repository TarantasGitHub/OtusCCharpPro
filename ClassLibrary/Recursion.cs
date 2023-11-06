using System.Collections.Concurrent;
using System.Diagnostics;

namespace Namespace;
public class Recursion
{

    public List<string> FilesWithRecursion { get; private set; }
    public long FilesWithRecursionMilliseconds { get; private set; }
    public BlockingCollection<string> FilesWithOutRecursion { get; private set; }
    public long FilesWithOutRecursionMilliseconds { get; private set; }

    public Recursion()
    {
        FilesWithRecursion = new List<string>();
        FilesWithOutRecursion = new BlockingCollection<string>();
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

    public async Task DeleteWithoutRecursion(DirectoryInfo baseDir)
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
        var tasks = new List<Task<(bool hasChild, IEnumerable<string> items)>>();
        do
        {
            currentLayer = new List<string>();
            hasChild = false;
            tasks = new List<Task<(bool hasChild, IEnumerable<string> items)>>();

            foreach (var path in folderTreeStack.Peek())
            {
                tasks.Add(PrepareFolderLayer(path, hasChild, options));
            }
            await Task.WhenAll(tasks);
            foreach (var task in tasks)
            {
                currentLayer.AddRange(task.Result.items);
                if (!hasChild && task.Result.hasChild)
                {
                    hasChild = true;
                }
            }
            folderTreeStack.Push(currentLayer.ToArray());
        } while (hasChild);

        while (folderTreeStack.Count > 0)
        {
            var folderTasks = new List<Task>();
            foreach (var path in folderTreeStack.Pop())
            {
                folderTasks.Add(FileProcessing(path, options));
            }
            await Task.WhenAll(folderTasks);
        }
        sw.Stop();
        FilesWithOutRecursionMilliseconds = sw.ElapsedMilliseconds;
    }

    private async Task FileProcessing(string path, EnumerationOptions options)
    {
        var di = new DirectoryInfo(path);
        if (di.Exists)
        {
            var files = di.GetFiles("*", options);
            var tasks = new List<Task>();
            foreach (var file in files)
            {
                tasks.Add(Task.Run(() =>
                {
                    if (!file.IsReadOnly)
                    {
                        DoSomeThingWithFile(string.Format("\t{0}", file.FullName), false);
                        // file.Delete();
                    }
                }));
            }
            await Task.WhenAll(tasks);
            DoSomeThingWithFile(di.FullName, false);
            // di.Delete();
        }
    }

    private static Task<(bool hasChild, IEnumerable<string> items)> PrepareFolderLayer(string path, bool hasChild, EnumerationOptions options)
    {
        var di = new DirectoryInfo(path);
        IEnumerable<string> items = Array.Empty<string>();

        if (di.Exists)
        {
            items = di.EnumerateDirectories("*", options).Select(d => d.FullName);

            if (!hasChild && items.Any())
            {
                hasChild = true;
            }
        }

        return Task.FromResult((hasChild, items));
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
