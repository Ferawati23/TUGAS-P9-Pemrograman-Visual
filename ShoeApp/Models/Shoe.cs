using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Shoe
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public string Color { get; set; }
    }
}
