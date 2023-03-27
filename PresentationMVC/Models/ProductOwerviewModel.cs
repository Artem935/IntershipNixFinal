using Domain.Models;

namespace PresentationMVC.Models
{
    public class ProductOwerviewModel
    {
        public Car Car { get; set; }
        public User User { get; set; }
        public ProductOwerviewModel() { }
        public ProductOwerviewModel(Car car, User user)
        {
            Car = car;
            User = user;
        }
    }
}
