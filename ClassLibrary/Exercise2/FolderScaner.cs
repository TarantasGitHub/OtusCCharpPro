namespace ClassLibrary.Exercise2
{
    public class FolderScaner
    {
        public event EventHandler<FindFileEventArgs>? FileFind;

        public Task Scan(string folderPath, CancellationToken cancellationToken = default)
        {
            if (Directory.Exists(folderPath))
            {
                var di = new DirectoryInfo(folderPath);
                foreach (var fi in di.GetFiles())
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    FileFind?.Invoke(this, new FindFileEventArgs { FileName = fi.Name });                   
                }
                return Task.CompletedTask;
            }
            throw new ArgumentException("Директория не существует");
        }
    }
}
