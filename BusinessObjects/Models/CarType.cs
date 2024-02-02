using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class CarType
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string TypeName { get; set; } = null!;

        [MaxLength(255)]
        public string? TypeDescription { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public IEnumerable<Car> Cars { get; set; } = Enumerable.Empty<Car>();
    }
}
