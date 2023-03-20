using Domain.Models;

namespace PresentationMVC.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public Menu Menu { get; set; }

        public HomeViewModel(IEnumerable<Car> cars , Menu menu) 
        {
            Cars = cars;
            Menu = menu;
        }

    }
}
