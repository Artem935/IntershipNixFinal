using Domain.Interfeces;
using Domain.Models;
using PresentationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SortingByMenu : ISortingByMenuRepository
    {
        public IEnumerable<Car> Sorting(IEnumerable<Car> cars, Menu menu)
        {
            if (menu.Searc is not null)
                cars = cars.Where(p => p.Brand.Contains(menu.Searc) || p.Model.Contains(menu.Searc));
            if (menu.MileageMax is not null)
                cars = cars.Where(p => p.CarMileage <= menu.MileageMax);
            if (menu.MileageMin is not null)
                cars = cars.Where(p => p.CarMileage >= menu.MileageMin);
            if (menu.PriceMax is not null)
                cars = cars.Where(p => p.Price <= menu.PriceMax);
            if (menu.PriceMin is not null)
                cars = cars.Where(p => p.Price >= menu.PriceMin);
            if (menu.YearMax is not null)
                cars = cars.Where(p => p.YearOfManufacture <= menu.YearMax);
            if (menu.YearMin is not null)
                cars = cars.Where(p => p.YearOfManufacture >= menu.YearMin);
            if (menu.GearboxType is not null && menu.GearboxType != "All")
                cars = cars.Where(p => p.GearboxType == menu.GearboxType);
            if (menu.Fuel is not null && menu.Fuel != "All")
                cars = cars.Where(p => p.FuelType == menu.Fuel);
            if (menu.Color is not null && menu.Color != "All")
                cars = cars.Where(p => p.Color == menu.Color);
            return cars;
        }
    }
}
