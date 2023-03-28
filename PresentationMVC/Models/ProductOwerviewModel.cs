using Domain.Models;

namespace PresentationMVC.Models
{
    public class ProductOwerviewModel
    {
        public Car Car { get; set; }
        public User User { get; set; }
        public int CountUserProduct { get; set; }

        public IEnumerable<Picture> Picture { get; set; }
        public ProductOwerviewModel() { }
        public ProductOwerviewModel(Car car, User user, int countUserProduct, IEnumerable<Picture> picture)
        {
            Car = car;
            User = user;
            CountUserProduct = countUserProduct;
            Picture = picture;  
        }
    }
}
