namespace DbClassLibrary.IdentityObjects
{
    public class Field
    {
        protected string Name { get; set; }
        protected string Operator { get; set; }
        protected List<(string name, string op, object value)> Comps { get; set; }
        protected bool InComplete { get; set; }

        // Задаем имя поля
        public Field(string name)
        {
            this.Name = name;
            this.Comps = new List<(string name, string op, object value)>();
        }

        // Ввести операцию и значение для тестирования
        // (например, больше 40), а также свойство Comps
        public void AddTest<T>(string op, T value) where T : notnull
        {
            Comps.Add(( name: this.Name, op: op, value: value));
        }

        // Comps - это массив, поэтому проверить одно поле
        // можно не одним, а несколькими способами
        public IEnumerable<(string name, string op, object value)> GetComps()
        {
            return Comps;
        }

        // Если массив Comps не содержит элементы,
        // это означает, что данные сравнения с полем и
        // само поле не готовы для применения в запросе
        public bool IsIncomplete()
        {
            return !this.Comps.Any();
        }
    }
}
