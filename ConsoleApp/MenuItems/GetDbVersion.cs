using ConsoleApp.DbCode;

namespace ConsoleApp.MenuItems
{
    internal class GetDbVersion : MenuItem, IMenuItem
    {
        public GetDbVersion(int itemNumber, Stack<MenuTypes> menuTypes) : this(itemNumber, "Получить версию базы данных", menuTypes) { }
        public GetDbVersion(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }
        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            Class1.GetVersion();
            Console.Read();
            Console.WriteLine();
            return base.Execute();
        }
    }
}
