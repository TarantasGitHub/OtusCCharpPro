namespace ClassLibrary.Exercise2
{
    public class FolderScaner
    {
        public event EventHandler<FindFileEventArgs>? FileFind;

        public void Scan(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                var di = new DirectoryInfo(folderPath);
                foreach (var fi in di.GetFiles())
                {
                    FileFind?.Invoke(this, new FindFileEventArgs { FileName = fi.Name });                   
                }
                return;
            }
            throw new ArgumentException("Директория не существует");
        }
    }
}
