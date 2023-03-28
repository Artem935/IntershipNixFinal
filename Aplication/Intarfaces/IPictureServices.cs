using Aplication.ViewModels;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Intarfaces
{
    public interface IPictureServices
    {
        public void Add(Picture picture );
        public void Remove(Picture picture);
        public void Overwriting(Picture picture);
    }
}
