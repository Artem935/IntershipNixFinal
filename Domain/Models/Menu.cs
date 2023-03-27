
namespace Domain.Models
{
    public class Menu
    {

        public int? PriceMax { get; set; }
        public int? PriceMin { get; set; }
        public string? Searc { get; set; }
        public int? MileageMax { get; set; }
        public int? MileageMin { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public string? GearboxType { get; set; }
        public string? Fuel { get; set; }
        public string? Color { get; set; }
    }
}