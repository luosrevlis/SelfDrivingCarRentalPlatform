using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Location))]
        public int LocationId { get; set; }

        [ForeignKey(nameof(Models.DrivingLicense))]
        public int? DrivingLicenseId { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Fullname { get; set; } = null!;

        [Required, MinLength(3), MaxLength(50)]
        public string Email { get; set; } = null!;

        [Required, MinLength(8), MaxLength(255)]
        public string Password { get; set; } = null!; // Consider password hash

        [MinLength(9), MaxLength(15)]
        public string? Phone { get; set; }

        [MinLength(1), MaxLength(255)]
        public string? Address { get; set; }

        [Required]
        public UserRole Role { get; set; }
        public string? ImagePath { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Location? Location { get; set; } = null!;

        public DrivingLicense? DrivingLicense { get; set; }

        public ICollection<Car>? Cars { get; set; } = new List<Car>();

        public ICollection<Contract>? Contracts { get; set; } = new List<Contract>();
    }
}
