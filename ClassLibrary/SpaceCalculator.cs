using System.Diagnostics;

namespace ClassLibrary
{
    public static class SpaceCalculator
    {
        public static async Task<(List<(string fileName, int spaceCount, long milliseconds)> fileInfoes, long totalMilliseconds)> CalcSpacesinDirectoryByFileListAsync(
            string folderPath,
            List<(string fileName, string fileAlias)> filePaths,
            CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new Exception($"Directory {folderPath} not found");
            }
                        
            if (filePaths == null || !filePaths.Any())
            {
                var di = new DirectoryInfo(folderPath);
                filePaths = di.GetFiles("*.txt").Select(fi => (fi.Name, fi.Name)).ToList();
                filePaths.AddRange(di.GetFiles("*.csv").Select(fi => (fi.Name, fi.Name)).ToList());
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var tasks = new List<Task<(string fileName, int spaceCount, long milliseconds)>>(capacity: filePaths.Count);

            foreach (var filePath in filePaths)
            {
                tasks.Add(CalcSpacesInFileAsync(filePath.fileName, Path.Combine(folderPath, filePath.fileAlias), cancellationToken));
            }

            await Task.WhenAll(tasks);
            stopWatch.Stop();

            return (tasks.Select(t => t.Result).ToList(), stopWatch.ElapsedMilliseconds);
        }

        public static async Task<(string fileName, int spaceCount, long milliseconds)> CalcSpacesInFileAsync(string fileName, string filePath, CancellationToken cancellationToken = default)
        {
            int count = 0;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            if (System.IO.File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (true)
                    {
                        var line = await reader.ReadLineAsync(cancellationToken);
                        if (line == null || cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }
                        foreach (var ch in line.ToCharArray())
                        {
                            if (ch == ' ')
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            stopWatch.Stop();
            return new(fileName, count, stopWatch.ElapsedMilliseconds);
        }
    }
}