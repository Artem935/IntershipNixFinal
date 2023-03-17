using Aplication.Interfaces;
using Aplication.ViewModels;
using Domain.Interfaces;
using Domain.Models;

namespace Aplication.Services
{
    public class CarServices: ICarServices
    {
        private ICarRepository _carRepository;
        public CarServices(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public void Add(Car car)
        {
            _carRepository.Add(car);
        }
        public ViewShopModels GetViewCar()
        {
            return new ViewShopModels
            {
                Cars = _carRepository.GetCar()
            };
        }
    }
}
