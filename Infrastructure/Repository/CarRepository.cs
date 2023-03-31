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
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public IEnumerable<Car> GetCar()
        {
            return _context.Cars;
        }

        public void Overwriting(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }

        public bool Remove(Car car)
        {
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return true;
        }
    }
}
