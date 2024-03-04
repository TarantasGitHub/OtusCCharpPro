using System.Reflection;
using System.Text;
using ClassLibrary.SpecialSymbols.Delimeters;

namespace ClassLibrary.Serializers
{
    public class CustomJsonSerializer : BaseSerializer
    {
        public CustomJsonSerializer()
        {
            this.SerializeContainer = new ClassLibrary.SpecialSymbols.Containers.Braces();
            this.NameValueDelimeter = new Colon();
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

        

        
    }
}
