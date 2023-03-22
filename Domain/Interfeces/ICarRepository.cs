using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCar();
        public void Add(Car car);
        public void Remove(Car car);

        public void Overwriting(Car car);
    }
}
