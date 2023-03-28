﻿using Data.Context;
using Domain.Interfeces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private ShopDbContext _context;

        public UserRepository(ShopDbContext context)
        {

            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Overwriting(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Remove(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
