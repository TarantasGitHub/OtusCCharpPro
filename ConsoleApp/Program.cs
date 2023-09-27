// See https://aka.ms/new-console-template for more information
using ConsoleApp.MenuItems;

bool continueWhile = false;
var menuTypes = new Stack<MenuTypes>(new[] { MenuTypes.MainMenu });

do
{
    foreach (var menuItem in MenuItemsFactory.GetItems(menuTypes))
    {
        Console.WriteLine("{0} {1}", menuItem.ItemNumber, menuItem.ItemText);
    }

    Console.Write("Введите номер выбранного пункта:");
    var userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
    {
        userInput = Console.ReadLine();
    }

    if (!string.IsNullOrEmpty(userInput)
        && int.TryParse(userInput, out int itemIndex)
        && itemIndex > 0
        && itemIndex <= MenuItemsFactory.GetItems(menuTypes).Count())
    {
        var item = MenuItemsFactory.GetItems(menuTypes).ElementAt(itemIndex - 1);
        Console.WriteLine("Выбран вариант: {0}\n", item.ItemText);
        var actionResult = item.Execute();
        continueWhile = actionResult.continueWhile;
        menuTypes = actionResult.menuTypes;
    }
    else
    {
        Console.Clear();
        Console.WriteLine("Выбран неправильный вариант перехода...");
        Console.WriteLine();
        continueWhile = true;
    }

} while (continueWhile);

