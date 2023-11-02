// See https://aka.ms/new-console-template for more information
using ClassLibrary;

Console.WriteLine("Введите путь к папке с файлами:");
var folderPath = Console.ReadLine();
if (folderPath != null)
{
    var result = await SpaceCalculator.CalcSpacesinDirectoryByFileListAsync(folderPath, new List<(string fileName, string fileAlias)>());
    
    foreach (var file in result.fileInfoes)
    {
        Console.WriteLine("В файле '{0}' найдено '{1}' пробелов за '{2}' миллисекунд", file.fileName, file.spaceCount, file.milliseconds);
    }
    Console.WriteLine("На обработку всех файлов потрачено '{0}' миллисекунд", result.totalMilliseconds);
}
Console.WriteLine("Для выхода нажмите любую клавишу");
Console.Read();

