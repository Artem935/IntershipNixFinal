using Domain.Models;

namespace PresentationMVC.Models
{
    public class ProductOwerviewModel
    {
        public IEnumerable<Car> Car { get; set; }
        public User User { get; set; }
        public ProductOwerviewModel() { }
        public ProductOwerviewModel(IEnumerable<Car> car, User user)
        {
            Car = car;
            User = user;
        }
    }
}
