using DbClassLibrary.IdentityObjects;
using System.Text;

namespace DbClassLibrary.UpdateFactories
{
    public abstract class UpdateFactory<T> where T : IdentityObject<T>
    {
        abstract public (string query, object[] values) NewUpdate(T obj);
        //abstract public function newUpdate(DomainObject $obj): array;

        protected (string where, object[] values) BuildStatement(string table, Dictionary<string, object> fields, Dictionary<string, object>? conditions = null)
        {
            var terms = new List<object>();
            var query = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (conditions != null)
            {
                sb.AppendLine($"UPDATE {table} SET ");
                sb.Append(string.Join(" = ?, ", fields.Keys));
                sb.AppendLine(" = ?");

                terms.AddRange(fields.Values);
                var cond = new List<string>(capacity: conditions.Count);
                sb.AppendLine(" WHERE ");

                foreach (var kvp in conditions)
                {
                    cond.Add($"{kvp.Key} = ?");
                    terms.Add(kvp.Value);
                }
                sb.AppendLine(string.Join(" AND ", cond));
                query = sb.ToString();
            }
            else
            {
                sb.AppendLine($"INSERT INTO {table} (");
                sb.AppendLine(string.Join(",", fields.Keys));
                sb.Append(") VALUES (");

                var qs = new List<string>(capacity: fields.Count);
                foreach (var kvp in fields)
                {
                    terms.Add(kvp.Value);
                    qs.Add("?");
                }
                sb.Append(string.Join(",", qs));
                sb.AppendLine(")");
                query = sb.ToString();
            }

            return (query, terms.ToArray());
        }
    }
}
