using Domain.Models;
using PresentationMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfeces
{
    public interface ISortingByMenuRepository
    {
        public IEnumerable<Car> Sorting(IEnumerable<Car> cars, Menu menu);
    }
}
