using Domain.Interfaces;
using Data.Context;
using Domain.Models;

namespace Data.Repository
{
    public class CarRepository : ICarRepository
    {
        private ShopDbContext _context;
        public CarRepository(ShopDbContext context)
        {

            _context = context;
        }

        public void Add(Car car)
        {
            _context.Add(car);
        }

        public IEnumerable<Car> GetCar()
        {
            return _context.Cars;
        }

    }
}
