using Aplication.Intarfaces;
using Aplication.Interfaces;
using Aplication.ViewModels;
using Domain.Interfaces;
using Domain.Interfeces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;
        public UserServices(IUserRepository carRepository)
        {
            _userRepository = carRepository;
        }
        public void Add(User user)
        {
            _userRepository.Add(user);
        }

        public void Remove(User user)
        {
            _userRepository.Remove(user);
        }

        public void Overwriting(User user)
        {
            _userRepository.Overwriting(user);
        }
    }
}
