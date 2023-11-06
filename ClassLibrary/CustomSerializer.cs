using System.Reflection;
using System.Text;
using ClassLibrary.Delimeters;

namespace ClassLibrary
{
    public class CustomSerializer
    {
        public string Serialize<T>(T obj)
        {
            return Serialize(obj, false);
        }

        public T Deserialize<T>(string json) where T : new()
        {
            var result = new T();
            var fields = result.GetType().GetFields();
            var properties = result.GetType().GetProperties();
            var delimeter = new UnknownDelimeter();
            delimeter.Parse(json);
            var stack = delimeter.Childs;
            while (stack != null && stack.Count > 0)
            {
                var topDelimeter = stack.Pop();
                var dict = new Dictionary<string, string>();
                if (topDelimeter is Braces braces)
                {
                    var prevItem = braces;
                    while (braces.Childs != null && braces.Childs.Count > 0)
                    {
                        string value;
                        var topItem = braces.Childs.Pop();
                        if (topItem is DoubleQuotes dq)
                        {
                            value = json.Substring((int)dq.StartIndex, (int)(dq.EndIndex - dq.StartIndex));
                        }
                        else if (topItem is Colon c)
                        {
                            value = json.Substring((int)c.StartIndex, (int)(prevItem.EndIndex - c.StartIndex));
                        }
                    }
                }
            }
            return result;
        }

        private string Serialize<T>(T obj, bool isChildObject)
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
            var sb = new StringBuilder("{");
            foreach (var field in fields)
            {
                sb.Append(string.Format("\"{0}\":{1},", field.Name, getFieldStringValue(obj, field)));
            }
            if (fields.Length > 0 && properties.Length == 0)
            {
                sb.Length -= 1;
            }
            foreach (var property in properties)
            {
                sb.Append(string.Format("\"{0}\":{1},", property.Name, getPropertyStringValue(obj, property)));
            }
            if (properties.Length > 0)
            {
                sb.Length -= 1;
            }
            sb.Append("}");
            return sb.ToString();
        }

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
