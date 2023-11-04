// See https://aka.ms/new-console-template for more information
using ClassLibrary.Exercise1;
using ClassLibrary.Exercise2;

StartMenu();

void StartMenu()
{
    Console.WriteLine("Введите номер задания:");
    Console.WriteLine("1. Поиск максимального значения");
    Console.WriteLine("2. Вывод списка файлов через событие");
    Console.WriteLine("3. Выход из приложения");
    var choosenValue = Console.ReadLine();

    switch(choosenValue)
    {
        case "1":
            FirstExercise();
            break;
        case "2":
            SecondExercise();
            break;
        case "3":
            CloseApp();
            break;
        default:
            UnKnownChoice();
            break;
    };
    return;
}

void FirstExercise()
{
    Console.WriteLine("Введите строковые значения через пробел сравнение будт по длине:");
    var insertedValue = Console.ReadLine();
    if (!string.IsNullOrEmpty(insertedValue))
    {
        var maxValue = Class1.GetMax(insertedValue.Split(' '), (string s) => (float)s.Length);
        Console.WriteLine("Максимальное значение: {0}", maxValue);
    }
    else
    {
        Console.WriteLine("Введена пустая строка");
    }
    UnKnownChoice();
}

void SecondExercise()
{
    Console.WriteLine("Введите путь к папке:");
    var folderPath = Console.ReadLine();

    if (string.IsNullOrEmpty(folderPath))
    {
        Console.WriteLine("Введен пустой путь.");
    }
    else
    {
        if (Directory.Exists(folderPath))
        {
            var fs = new FolderScaner();
            fs.FileFind += EventAction;

            fs.Scan(folderPath);
        }
    }
    UnKnownChoice();
}

void CloseApp()
{
    return;
}

void UnKnownChoice()
{
    Console.WriteLine("Вернуться в начало (y):");
    if(Console.ReadLine() == "y")
    {
        StartMenu();
    }
}

void EventAction(object sender, FindFileEventArgs e)
{
    Console.WriteLine("Найден файл '{0}'", e.FileName);
}