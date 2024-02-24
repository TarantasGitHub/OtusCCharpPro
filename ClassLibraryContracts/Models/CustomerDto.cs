using System.ComponentModel.DataAnnotations;

namespace ClassLibraryContracts.Models
{
    public class CustomerDto : BaseEntityDto<Int64>
    {
        public string? FirstName { get; init; }

        public string? LastName { get; init; }
    }
}
