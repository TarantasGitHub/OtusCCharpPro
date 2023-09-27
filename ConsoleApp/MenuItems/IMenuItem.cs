namespace ConsoleApp.MenuItems
{
    internal interface IMenuItem
    {
        Stack<MenuTypes> CurrentMenuTypes { get; }
        int ItemNumber { get; }
        string ItemText { get; }
        (bool continueWhile, Stack<MenuTypes> menuTypes) Execute();
    }
}
