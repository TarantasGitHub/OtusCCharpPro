namespace ConsoleApp.MenuItems
{
    internal class NavigationItem : MenuItem, IMenuItem
    {
        public MenuTypes ToMenuTypes { get; private set; }
        public NavigationItem(int itemNumber, string itemText, Stack<MenuTypes> menuTypes, MenuTypes toMenuTypes) : base(itemNumber, itemText, menuTypes)
        {
            ToMenuTypes = toMenuTypes;
        }

        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            CurrentMenuTypes.Push(ToMenuTypes);
            return base.Execute();
        }
    }
}
