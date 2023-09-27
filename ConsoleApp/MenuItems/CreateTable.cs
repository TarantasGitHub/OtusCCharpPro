using ConsoleApp.DbCode;

namespace ConsoleApp.MenuItems
{
    internal class CreateTable : MenuItem, IMenuItem
    {
        public CreateTable(int itemNumber, Stack<MenuTypes> menuTypes) : base(itemNumber, "Добавить таблицу", menuTypes) { }
        public CreateTable(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }

        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            Class1.AddTableAircrafts().Wait();
            Console.Read();
            return base.Execute();
        }
    }
}
