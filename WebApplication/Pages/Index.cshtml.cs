using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IWebHostEnvironment environment, ILogger<IndexModel> logger)
        {
            _environment = environment;
            _logger = logger;
            UploadedFileInfo = new List<(string fileName, int spaceCount, long milliseconds)>();
        }

        public void OnGet()
        {

        }

        [BindProperty]
        public List<IFormFile> Upload { get; set; }

        [BindProperty]
        public List<(string fileName, int spaceCount, long milliseconds)> UploadedFileInfo { get; set; }

        [BindProperty]
        public long TotalMilliseconds { get; set; }

        public async Task OnPost(CancellationToken cancellationToken = default)
        {
            var sessionSuffix = Guid.NewGuid().ToString().Replace("-", "");
            var uploadeFolderPath = Path.Combine(_environment.ContentRootPath, @"UploadedFiles");
            checkDirectory(uploadeFolderPath);
            var filePaths = new List<(string fileName, string filePath)>();
            foreach (var file in Upload)
            {
                if (file != null)
                {
                    if (file.Length > 0 &&
                        (file.ContentType.Contains("text/plain") ||
                         file.ContentType.Contains(".csv")))
                    {
                        var filePath = file.FileName + "_" + sessionSuffix;
                        var fullFilePath = Path.Combine(uploadeFolderPath, filePath);
                        using (var fileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream, cancellationToken);
                            filePaths.Add((file.FileName, filePath));
                        }
                    }
                }
            }
            var result = await SpaceCalculator.CalcSpacesinDirectoryByFileListAsync(uploadeFolderPath, filePaths, cancellationToken);
            UploadedFileInfo = result.fileInfoes;
            TotalMilliseconds = result.totalMilliseconds;
            DeleteTempFiles(uploadeFolderPath, filePaths.Select(f => f.filePath).ToList());
            return;
        }

        private void checkDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void DeleteTempFiles(string folderPath, List<string> filePaths)
        {
            if (Directory.Exists(folderPath))
            {
                foreach (var filePath in filePaths)
                {
                    var fullFilePath = Path.Combine(folderPath, filePath);
                    if (System.IO.File.Exists(fullFilePath))
                    {
                        System.IO.File.Delete(fullFilePath);
                    }
                }
            }
        }
    }
}