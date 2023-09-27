using ConsoleApp.DbCode;

namespace ConsoleApp.MenuItems
{
    internal class AllTableList : MenuItem, IMenuItem
    {
        public AllTableList(int itemNumber, Stack<MenuTypes> menuTypes) : this(itemNumber, "Вывести список всех таблиц", menuTypes) { }
        public AllTableList(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }
        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            Class1.GetAllTable();
            Class1.GetTableData("public.aircrafts");
            Console.Read();
            Console.WriteLine();
            return base.Execute();
        }
    }
}
