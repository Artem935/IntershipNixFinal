using Data.Context;
using Domain.Interfeces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PictureRepository : IPictureRepository
    {
        private ShopDbContext _context;
        public PictureRepository(ShopDbContext context)
        {

            _context = context;
        }

        public void Add(Picture picture )
        {
            _context.Picture.Add(picture);
            _context.SaveChanges();
        }

        public IEnumerable<Car> GetCar()
        {
            return _context.Cars;
        }

        public void Overwriting(Picture picture)
        {
            _context.Picture.Update(picture);
            _context.SaveChanges();
        }

        public void Remove(Picture picture)
        {
            _context.Picture.Remove(picture);
            _context.SaveChanges();
        }

    }
}
