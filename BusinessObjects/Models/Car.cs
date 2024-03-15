using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
    [Index(nameof(PlateNumber), IsUnique = true)]
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int CarOwnerId { get; set; }

        [Required]
        [ForeignKey(nameof(Models.CarType))]
        public int CarTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(Models.CarBrand))]
        public int CarBrandId { get; set; }

        [Required, MaxLength(100)]
        public string CarModel { get; set; } = null!;

        [Required, MinLength(8), MaxLength(12)]
        public string PlateNumber { get; set; } = null!;

        [Required, MinLength(6), MaxLength(6)]
        public string CarRegisterNumber { get; set; } = null!;

        [Required, Range(500000, 10000000)]
        public double PricePerDay { get; set; }

        [Required]
        public bool IsElectric { get; set; }

        [Required]
        public bool IsMortgageRequired { get; set; }

        [Required, Range(0, 30)]
        public int DepositRatio { get; set; }

        [Required, MaxLength(100)]
        public string PickupLocation { get; set; } = null!;

        [Required]
        public CarStatus Status { get; set; } = CarStatus.Available;

        [Required]
        public bool IsDeleted { get; set; } 

        public User? CarOwner { get; set; } = null!;

        public CarType? CarType { get; set; } = null!;

        public CarBrand? CarBrand { get; set; } = null!;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
