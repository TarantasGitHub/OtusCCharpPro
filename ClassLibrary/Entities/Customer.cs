using ClassLibraryContracts.Entities;

namespace ClassLibrary.Entities
{
    public class Customer : BaseEntity<long>
    {
        public string Firstname { get; init; }

        public string Lastname { get; init; }
    }
}
