namespace DbClassLibrary.IdentityObjects
{
    internal abstract class IdentityObject
    {
        protected Field CurrentField { get; set; }
        protected Dictionary<string, Field> Fields { get; set; }
        private string[] enForce = new string[0]; 

        // Объект идентичности может быть создан пустым
        // млм же с отдельным полем
        public IdentityObject(string field = null, string[] enForce = null)
        {
            if(enForce != null)
            {
                this.enForce = enForce;
            }

            if(field != null)
            {
                Field(field);
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
        public IdentityObject Field(string fieldName)
        {
            if(!IsVoid() && this.CurrentField.IsIncomplete())
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
            return this;
        }

        // Имеются ли уже какие-нибудь поля
        // у объекта идентичности?
        public bool IsVoid()
        {
            return this.Fields.Any();
        }

        // Допустимо ли заданное имя поля?
        public void EnforceField(string fieldName)
        {
            if(!this.enForce.Any() && !this.enForce.Contains(fieldName))
            {
                var forcelist = string.Join(',', this.enForce);
                throw new ArgumentException($"{fieldName} не является корректным полем {forcelist}");
            }
        }

        // Вводит операцию равенства в текущее поле,
        // например, значение 'age' превращается
        // в значение 'age=40'. Возвращает ссылку на
        // текущий объект через метод operator()
        public IdentityObject eq<T>(T value) where T : notnull
        {
            return this.Operator("=", value);
        }

        // Операция сравнения "меньше"
        public IdentityObject lt<T>(T value) where T : notnull
        {
            return this.Operator("<", value);
        }

        // Операция сравнения "больше"
        public IdentityObject gt<T>(T value) where T : notnull
        {
            return this.Operator(">", value);
        }

        // Выполняет подготовку к операциям с поляем.
        // Получает текущее поле и вводит операцию
        // и проверяемое значение
        private IdentityObject Operator<T>(string symbol, T value) where T : notnull
        {
            if (this.IsVoid())
            {
                throw new Exception("Поле не определено");
            }
            this.CurrentField.AddTest(symbol, value);

            return this;
        }
    }
}
