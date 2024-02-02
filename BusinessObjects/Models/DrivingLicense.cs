using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
    public class DrivingLicense
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int OwnerId { get; set; }

        [Required, MinLength(8), MaxLength(12)]
        public string DrivingLicenseNumber { get; set; } = null!;

        [Required]
        public DateTime ExpiryDate { get; set; } = new DateTime();

        [Required]
        public bool IsDeleted { get; set; }

        public User Owner { get; set; } = null!;
    }
}
