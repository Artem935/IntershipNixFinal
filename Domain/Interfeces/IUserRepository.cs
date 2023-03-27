using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfeces
{
    public interface IUserRepository
    {
        public void Add(User user);
        public void Remove(User user);

        public void Overwriting(User user);
    }
}
