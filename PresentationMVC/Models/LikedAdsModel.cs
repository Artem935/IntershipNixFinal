using Domain.Models;

namespace PresentationMVC.Models
{
    public class LikedAdsModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public Menu Menu { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<Picture> Picture { get; set; }

        public IEnumerable<Liked> Liked { get; set; }
        public LikedAdsModel() { }
        public LikedAdsModel(IEnumerable<Car> cars, Menu menu, Pager pager, IEnumerable<Picture> picture, IEnumerable<Liked> liked)
        {
            Cars = cars;
            Menu = menu;
            Pager = pager;
            Picture = picture;
            Liked = liked;
        }
    }
}
