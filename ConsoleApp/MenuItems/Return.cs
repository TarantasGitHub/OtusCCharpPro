namespace ConsoleApp.MenuItems
{
    internal class Return : MenuItem, IMenuItem
    {
        public Return(int itemNumber, Stack<MenuTypes> menuTypes) : this(itemNumber, "Вернуться", menuTypes) { }
        public Return(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }
        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            var currentMenu = CurrentMenuTypes.Pop();
            return base.Execute();
        }
    }
}
