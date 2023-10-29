namespace DbClassLibrary.IdentityObjects
{
    public abstract class IdentityObject<TSourse> where TSourse : IdentityObject<TSourse>
    {
        protected Field CurrentField { get; set; }
        protected Dictionary<string, Field> Fields { get; set; }

        private string[] enForce = Array.Empty<string>();

        // Объект идентичности может быть создан пустым
        // млм же с отдельным полем
        public IdentityObject(string field = null, string[] enForce = null)
        {
            if (enForce != null)
            {
                this.enForce = enForce;
            }

            if (field != null)
            {
                Field(field);
            }
            else
            {
                Fields = new Dictionary<string, Field>();
            }
        }

        // Имена полей, на которые наложено данное ограничение
        public string[] GetObjectFields()
        {
            return this.enForce;
        }

        // Вводит новое поле.
        // Генерирует ошибку, если текущее поле не заполнено
        // (т.е. age, а не age > 40).
        // Этот метод возвращает ссылку на текущий объект
        // и тем самым разрешает текучий синтаксис
        public TSourse Field(string fieldName)
        {
            if (!IsVoid() && this.CurrentField.IsIncomplete())
            {
                throw new ArgumentException("Неполное поле");
            }

            EnforceField(fieldName);

            if (this.Fields.ContainsKey(fieldName))
            {
                this.CurrentField = this.Fields[fieldName];
            }
            else
            {
                this.CurrentField = new Field(fieldName);
                this.Fields.Add(fieldName, this.CurrentField);
            }
            return (TSourse)this;
        }

        // Имеются ли уже какие-нибудь поля
        // у объекта идентичности?
        public bool IsVoid()
        {
            return !this.Fields.Any();
        }

        // Допустимо ли заданное имя поля?
        public void EnforceField(string fieldName)
        {
            if (!this.enForce.Any() && !this.enForce.Contains(fieldName))
            {
                var forcelist = string.Join(',', this.enForce);
                throw new ArgumentException($"{fieldName} не является корректным полем {forcelist}");
            }
        }

        // Вводит операцию равенства в текущее поле,
        // например, значение 'age' превращается
        // в значение 'age=40'. Возвращает ссылку на
        // текущий объект через метод operator()
        public TSourse eq<T>(T value) where T : notnull
        {
            return this.Operator("=", value);
        }

        // Операция сравнения "меньше"
        public TSourse lt<T>(T value) where T : notnull
        {
            return this.Operator("<", value);
        }

        // Операция сравнения "больше"
        public TSourse gt<T>(T value) where T : notnull
        {
            return this.Operator(">", value);
        }

        // Выполняет подготовку к операциям с поляем.
        // Получает текущее поле и вводит операцию
        // и проверяемое значение
        private TSourse Operator<T>(string symbol, T value) where T : notnull
        {
            if (this.IsVoid())
            {
                throw new Exception("Поле не определено");
            }
            this.CurrentField.AddTest(symbol, value);

            return (TSourse)this;
        }

        // Возвращает все полученные до сих пор результаты
        // сравнения из ассоциативного массива
        public IEnumerable<(string name, string op, object value)> GetComps()
        {
            var result = new List<(string name, string op, object value)>(capacity: getAllCompsCapacity());

            foreach (var field in Fields)
            {
                result.AddRange(field.Value.GetComps());
            }

            return result;
        }

        private int getAllCompsCapacity()
        {
            var result = 0;

            foreach (var field in this.Fields)
            {
                ++result;
                result += field.Value.GetComps().Count();
            }

            return result;
        }
    }
}
