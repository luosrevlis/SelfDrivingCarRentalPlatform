using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(255)]
        public string LocationName { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
