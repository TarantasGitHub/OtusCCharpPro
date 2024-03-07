using System.Reflection;
using System.Text;
using ClassLibrary.SpecialSymbols;
using ClassLibrary.SpecialSymbols.Delimeters;

namespace ClassLibrary.Serializers
{
    public class CustomJsonSerializer : BaseSerializer
    {
        public CustomJsonSerializer()
        {
            this.SerializeContainer = new ClassLibrary.SpecialSymbols.Containers.BracesContainer();
            this.NameValueDelimeter = new SpecialSymbols.Colon();
        }

        public override T Deserialize<T>(string json)
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
                        if (topItem is SpecialSymbols.Delimeters.DoubleQuotes dq)
                        {
                            value = json.Substring((int)dq.StartIndex, (int)(dq.EndIndex - dq.StartIndex));
                        }
                        else if (topItem is SpecialSymbols.Delimeters.Colon c)
                        {
                            value = json.Substring((int)c.StartIndex, (int)(prevItem.EndIndex - c.StartIndex));
                        }
                    }
                }
            }
            return result;
        }
    }
}
