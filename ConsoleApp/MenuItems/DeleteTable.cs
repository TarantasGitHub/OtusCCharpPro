namespace ConsoleApp.MenuItems
{
    internal class DeleteTable : MenuItem, IMenuItem
    {
        public DeleteTable(int itemNumber, Stack<MenuTypes> menuTypes) : this(itemNumber, "Удалить таблицу", menuTypes) { }
        public DeleteTable(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }
    }
}
