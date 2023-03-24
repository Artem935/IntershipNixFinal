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
            _context.SaveChanges();
        }

        public IEnumerable<Car> GetCar()
        {
            return _context.Cars;
        }

        public void Overwriting(Car car)
        {
            _context.Update(car);
            _context.SaveChanges();
        }

        public void Remove(Car car)
        {
            _context.Remove(car);
            _context.SaveChanges();
        }
    }
}
