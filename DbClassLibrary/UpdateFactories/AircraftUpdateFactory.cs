namespace DbClassLibrary.UpdateFactories
{
    public class AircraftUpdateFactory : UpdateFactory
    {
        public override (string query, object[] values) NewUpdate<Aircraft, String>(Aircraft obj)
        {
            var id = obj.GetId();
            Dictionary<string, object>? cond = null;
            var values = new Dictionary<string, object>();

            foreach (var kvp in obj.GetPropertiesWithVAlues())
            {
                values.Add(kvp.Key, kvp.Value);
            }

            if (!string.IsNullOrEmpty(obj.GetId().ToString()) && !obj.IsNew)
            {
                cond = new Dictionary<string, object>
                {
                    { obj.GetKeyName(), obj.GetId() }
                };
            }
                        
            return this.BuildStatement("aircrafts", values, cond);
        }
    }
}
