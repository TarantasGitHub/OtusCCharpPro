using DbClassLibrary.IdentityObjects;

namespace DbClassLibrary.SelectionFactories
{
    public abstract class SelectionFactory<T> where T : IdentityObject<T>
    {
        abstract public (string query, object[] values) NewSelection(T obj);

        protected (string where, object[] values) BuildWhere(T obj)
        {
            if (obj.IsVoid())
            {
                return (string.Empty, Array.Empty<string>());
            }

            var compstrings = new List<string>(capacity: obj.GetComps().Count());
            var values = new List<object>(capacity: obj.GetComps().Count());

            foreach (var comp in obj.GetComps())
            {
                compstrings.Add($"\"{comp.name}\" {comp.op} ?");
                values.Add(comp.value);
            }

            var whereStr = compstrings.Any()
                ? $"\nWHERE {string.Join(" AND ", compstrings)}"
                : string.Empty;

            return (whereStr, values.ToArray());
        }
    }
}
