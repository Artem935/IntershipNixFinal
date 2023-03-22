using Aplication.ViewModels;
using Domain.Models;


namespace Aplication.Interfaces
{
    public interface ICarServices
    {
        public ViewShopModels GetViewCar();
        public void Add(Car car);
        public void Remove(Car car);
        public void Overwriting(Car car);

    }
}
