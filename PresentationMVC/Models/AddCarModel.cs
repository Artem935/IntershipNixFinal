using Domain.Models;

namespace PresentationMVC.Models
{
    public class AddCarModel
    {
        public Car Car { get; set; }
        public Picture Picture { get; set; }
        public IFormFile TakeImage { get; set; }
        
    }
}
