using DbClassLibraryContracts.DomainObjects;
using System.Text;

namespace DbClassLibrary.UpdateFactories
{
    public abstract class UpdateFactory
    {
        public abstract(string query, object[] values) NewUpdate<T, TKey>(T obj) where T : DomainObject<TKey>;

        protected (string where, object[] values) BuildStatement(string table, Dictionary<string, object> fields, Dictionary<string, object>? conditions = null)
        {
            var terms = new List<object>();
            var query = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (conditions != null)
            {
                sb.AppendLine($"UPDATE \"{table}\" SET ");
                sb.Append(string.Format("\"{0}\"", string.Join("\" = ?, \"", fields.Keys)));
                sb.AppendLine(" = ?");

                terms.AddRange(fields.Values);
                var cond = new List<string>(capacity: conditions.Count);
                sb.Append("WHERE ");

                foreach (var kvp in conditions)
                {
                    cond.Add($"\"{kvp.Key}\" = ?");
                    terms.Add(kvp.Value);
                }
                sb.AppendLine(string.Join(" AND ", cond));
                query = sb.ToString();
            }
            else
            {
                sb.AppendLine($"INSERT INTO \"{table}\" (");
                sb.AppendLine(string.Format("\"{0}\"", string.Join("\", \"", fields.Keys)));
                sb.Append(") VALUES (");

                var qs = new List<string>(capacity: fields.Count);
                foreach (var kvp in fields)
                {
                    terms.Add(kvp.Value);
                    qs.Add("?");
                }
                sb.Append(string.Join(", ", qs));
                sb.AppendLine(")");
                query = sb.ToString();
            }

            return (query, terms.ToArray());
        }
    }
}
