namespace DbClassLibrary.IdentityObjects
{
    internal class AircraftIdentityObject : IdentityObject
    {
        public AircraftIdentityObject(string field = null) : base(field, new[] { "AircraftCode", "Model", "Range" })
        {

        }
    }
}
