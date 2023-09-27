namespace ConsoleApp.MenuItems
{
    internal class AddRecordToTable : MenuItem, IMenuItem
    {
        public AddRecordToTable(int itemNumber, Stack<MenuTypes> menuTypes) : this(itemNumber, "Добавить запись в таблицу", menuTypes) { }
        public AddRecordToTable(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }
        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            if (this.CurrentMenuTypes.Peek() == MenuTypes.MainMenu)
            {
                this.CurrentMenuTypes.Push(MenuTypes.AllTableContentMenu);
            }
            return base.Execute();
        }
    }
}
