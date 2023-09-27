namespace ConsoleApp.MenuItems
{
    internal class MenuItemsFactory
    {
        public static IEnumerable<IMenuItem> GetItems(Stack<MenuTypes> menuTypes)
        {
            return menuTypes.Peek() switch
            {
                MenuTypes.MainMenu => MainMenu(menuTypes),
                MenuTypes.AllTableContentMenu => AllTableContentMenu(menuTypes),
                MenuTypes.AddRecordMenu => AddRecordMenu(menuTypes),
                _ => MainMenu(menuTypes)
            };
        }

        private static IEnumerable<IMenuItem> MainMenu(Stack<MenuTypes> menuTypes)
        {
            return new List<IMenuItem>()
            {
                new NavigationItem(1, "Работа со структурой Базы Данных", menuTypes, MenuTypes.AllTableContentMenu),
                new NavigationItem(2, "Работа с табличными данными", menuTypes, MenuTypes.AddRecordMenu),
                new Quit(3, menuTypes)
            };
        }

        private static IEnumerable<IMenuItem> AllTableContentMenu(Stack<MenuTypes> menuTypes)
        {
            return new List<IMenuItem>()
            {
                new GetDbVersion(1, menuTypes),
                new AllTableList(2, menuTypes),
                new CreateTable(3, menuTypes),
                new DeleteTable(4, menuTypes),
                new Return(5, menuTypes),
                new Quit(6, menuTypes)
            };
        }

        private static IEnumerable<IMenuItem> AddRecordMenu(Stack<MenuTypes> menuTypes)
        {
            return new List<IMenuItem>()
            {
                new AllTableList(1, menuTypes),
                new AddRecordToTable(2, menuTypes),
                new Return(3, menuTypes),
                new Quit(4, menuTypes)
            };
        }
    }
}
