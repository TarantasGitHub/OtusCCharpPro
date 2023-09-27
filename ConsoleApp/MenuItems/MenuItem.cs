namespace ConsoleApp.MenuItems
{
    internal abstract class MenuItem : IMenuItem
    {
        public Stack<MenuTypes> CurrentMenuTypes { get; private set; }
        public int ItemNumber { get; private set; }
        public string ItemText { get; private set; }

        protected MenuItem(int itemNumber, string itemText, Stack<MenuTypes> menuTypes)
        {
            ItemNumber = itemNumber;
            ItemText = itemText;
            CurrentMenuTypes = menuTypes;
        }

        public virtual (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            Console.Clear();
            return (true, CurrentMenuTypes);
        }
    }
}
