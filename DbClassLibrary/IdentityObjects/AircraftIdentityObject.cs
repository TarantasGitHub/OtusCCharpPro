namespace DbClassLibrary.IdentityObjects
{
    public class AircraftIdentityObject : IdentityObject<AircraftIdentityObject>
    {
        public AircraftIdentityObject(string field = null) : base(field, new[] { "AircraftCode", "Model", "Range" })
        {

        }
    }
}
