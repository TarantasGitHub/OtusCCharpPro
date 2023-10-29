using System.ComponentModel.DataAnnotations;

namespace DbClassLibrary.Entities
{
    public class Aircraft : BaseEntity<string>
    {
        [Required]
        [StringLength(3, MinimumLength = 1)]
        public string AircraftCode { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int Range { get; set; }
    }
}
