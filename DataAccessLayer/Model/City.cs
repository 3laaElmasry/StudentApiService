using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }

        [Required]
        public string? CityName { get; set; }
    }
}
