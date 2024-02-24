using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Models
{
    [Index(nameof(CarId), nameof(RentStartDate), IsUnique = true)]
    public class Contract
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int CustomerId { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Car))]
        public int CarId { get; set; }

        [Required]
        public DateTime RentStartDate { get; set; }

        [Required]
        public DateTime RentEndDate { get; set; }

        [Required]
        public DateTime SignDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public User Customer { get; set; } = null!;

        public Car Car { get; set; } = null!;

        public Transaction Transaction { get; set; } = null!;
    }
}
