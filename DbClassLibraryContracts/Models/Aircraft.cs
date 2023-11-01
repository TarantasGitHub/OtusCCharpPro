using DbClassLibraryContracts.DomainObjects;

namespace DbClassLibraryContracts.Models
{
    public class Aircraft : DomainObject<String>
    {
        public Aircraft() : this(string.Empty, string.Empty, 0) { }
        public Aircraft(string aircraftCode) : this(aircraftCode, string.Empty, 0) { }
        public Aircraft(string aircraftCode, string model) : this(aircraftCode, model, 0) { }
        public Aircraft(string aircraftCode, string model, int range) : base(aircraftCode, "AircraftCode")
        {
            this.AircraftCode = aircraftCode;
            this.Model = model;
            this.Range = range;
        }

        public string AircraftCode { get; set; }

        public string Model { get; set; }

        public int Range { get; set; }

        public override Dictionary<string, object> GetPropertiesWithVAlues()
        {
            return new Dictionary<string, object>()
            {
                { "AircraftCode", this.AircraftCode },
                { "Model", this.Model},
                { "Range", this.Range }
            };
        }
    }
}
