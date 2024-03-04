using ClassLibrary.SpecialSymbols.Containers;
using ClassLibrary.SpecialSymbols.Delimeters;
using System.Reflection;
using System.Text;

namespace ClassLibrary.Serializers
{
    public abstract class BaseSerializer
    {
        public BaseContainer SerializeContainer { get; init; }

        public Delimeter NameValueDelimeter { get; init; }

        public virtual string Serialize<T>(T obj)
        {
            return Serialize(obj, false);
        }

        protected virtual string Serialize<T>(T obj, bool isChildObject)
        {
            if (obj == null)
            {
                if (isChildObject)
                {
                    return "null";
                }
                throw new ArgumentNullException();
            }
            Type t = obj.GetType();
            var fields = t.GetFields();
            var properties = t.GetProperties();
            var sb = new StringBuilder(SerializeContainer.OpenningSymbol.ToString());
            foreach (var field in fields)
            {
                sb.Append(string.Format("\"{0}\"{1}{2},", field.Name, NameValueDelimeter.Symbol, getFieldStringValue(obj, field)));
            }
            if (fields.Length > 0 && properties.Length == 0)
            {
                sb.Length -= 1;
            }
            foreach (var property in properties)
            {
                sb.Append(string.Format("\"{0}\"{1}{2},", property.Name, NameValueDelimeter.Symbol, getPropertyStringValue(obj, property)));
            }
            if (properties.Length > 0)
            {
                sb.Length -= 1;
            }
            sb.Append(SerializeContainer.ClosingSymbol);
            return sb.ToString();
        }

        public abstract T Deserialize<T>(string json) where T : new();

        private string getFieldStringValue<T>(T obj, FieldInfo fieldInfo)
        {
            var value = fieldInfo?.GetValue(obj);
            if (value == null)
            {
                return "null";
            }
            return getValue(fieldInfo.FieldType, value);
        }

        private string getPropertyStringValue<T>(T obj, PropertyInfo propertyInfo)
        {
            var value = propertyInfo?.GetValue(obj);
            if (value == null)
            {
                return "null";
            }
            return getValue(propertyInfo.PropertyType, value);
        }

        private string getValue(Type type, object value)
        {
            switch (type.FullName)
            {
                case "System.Int32":
                    return value.ToString();
                default:
                    return string.Format("\"{0}\"", value.ToString());
            }
        }
    }
}
