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
        public bool Add(User user);
        public bool Remove(User user);

        public bool Overwriting(User user);
    }
}
