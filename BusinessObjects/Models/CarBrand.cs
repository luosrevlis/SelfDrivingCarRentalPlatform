using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class CarBrand
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(20)]
        public string BrandName { get; set; } = null!;

        [MaxLength(255)]
        public string? BrandDescription { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public IEnumerable<Car> Cars { get; set; } = Enumerable.Empty<Car>();
    }
}
