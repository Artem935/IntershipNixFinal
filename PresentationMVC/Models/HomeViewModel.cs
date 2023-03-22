using Domain.Models;

namespace PresentationMVC.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public Menu Menu { get; set; }
        public Pager Pager { get; set; }
        public HomeViewModel(){}
        public HomeViewModel(IEnumerable<Car> cars , Menu menu, Pager pager) 
        {
            Cars = cars;
            Menu = menu;
            Pager = pager;
        }

    }
}
