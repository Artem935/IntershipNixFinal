using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfeces
{
    public interface IPictureRepository
    {
        public void Add(Picture picture);
        public void Remove(Picture picture);
        public void Overwriting(Picture picture);
    }
}
