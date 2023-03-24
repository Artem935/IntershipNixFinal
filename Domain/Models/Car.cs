

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int CarMileage { get; set; }
        public int YearOfManufacture { get; set; }
        public float EngineCapacity { get; set; }
        public string FuelType { get; set; }
        public string GearboxType { get; set; }
        public string Description { get; set; }
    }
}
