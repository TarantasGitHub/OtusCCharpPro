using DbClassLibrary.IdentityObjects;

namespace DbClassLibrary.SelectionFactories
{
    public class AircraftSelectionFactory : SelectionFactory<AircraftIdentityObject>
    {
        public override (string query, object[] values) NewSelection(AircraftIdentityObject obj)
        {
            var fields = string.Join("\" ,\"", obj.GetObjectFields());
            var core = $"SELECT \"{fields}\" FROM \"aircrafts\"";
            var buildedWhere = this.BuildWhere(obj);

            return ($"{core}{buildedWhere.where}", buildedWhere.values);
        }
    }
}
