namespace ConsoleApp.MenuItems
{
    internal class Quit : MenuItem, IMenuItem
    {
        public Quit(int itemNumber, Stack<MenuTypes> menuTypes) : this(itemNumber, "Выйти", menuTypes) { }
        public Quit(int itemNumber, string itemText, Stack<MenuTypes> menuTypes) : base(itemNumber, itemText, menuTypes) { }
        public override (bool continueWhile, Stack<MenuTypes> menuTypes) Execute()
        {
            return (false, CurrentMenuTypes);
        }
    }
}
