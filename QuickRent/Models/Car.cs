using System.ComponentModel.DataAnnotations;

namespace QuickRent.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public CarsClasses CarsClasses { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        public Decimal Fuel { get; set; }
        [Required]
        public string ImageUrl { get; set; }


    }
}
