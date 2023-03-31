using Aplication.Services;
using Data.Context;
using Data.Repository;
using Domain.Interfaces;
using Domain.Interfeces;
using Domain.Models;
using Xunit;

namespace UnitTest
{
    public class Tests
    {
        [Fact]
        public void GetCar()
        {// arrege
            ShopDbContext shopDbContext = new ShopDbContext(options: new Microsoft.EntityFrameworkCore.DbContextOptions<ShopDbContext>());
            ICarRepository carRepository = new CarRepository(shopDbContext);
            CarServices carServices = new CarServices(carRepository);
            // action
            var expected = shopDbContext.Cars;
            var execute = carServices.GetViewCar();
            //assert
            Assert.Equals(expected, execute);
        }
        [Fact]
        public void RemoveCar()
        {// arrege
            ShopDbContext shopDbContext = new ShopDbContext(options: new Microsoft.EntityFrameworkCore.DbContextOptions<ShopDbContext>());
            ICarRepository carRepository = new CarRepository(shopDbContext);
            CarServices carServices = new CarServices(carRepository);
            // action
            int id = 1;
            Car car = shopDbContext.Cars.FirstOrDefault(p => p.Id == id);
            var execute = carServices.Remove(car);
            //assert
            Assert.True(execute);
        }
        [Fact]
        public void AddUser()
        {// arrege
            ShopDbContext shopDbContext = new ShopDbContext(options: new Microsoft.EntityFrameworkCore.DbContextOptions<ShopDbContext>());
            IUserRepository userRepository = new UserRepository(shopDbContext);
            UserServices userServices = new UserServices(userRepository);
            // action
            int id = 1;
            User car = shopDbContext.Users.FirstOrDefault(p => p.Id == id);
            var execute = userServices.Add(car);
            //assert
            Assert.True(execute);
        }
        [Fact]
        public void RemoveUser()
        {// arrege
            ShopDbContext shopDbContext = new ShopDbContext(options: new Microsoft.EntityFrameworkCore.DbContextOptions<ShopDbContext>());
            IUserRepository userRepository = new UserRepository(shopDbContext);
            UserServices userServices = new UserServices(userRepository);
            // action
            int id = 1;
            User car = shopDbContext.Users.FirstOrDefault(p => p.Id == id);
            var execute = userServices.Remove(car);
            //assert
            Assert.True(execute);
        }

        /*[Fact]
        public void Test1()
        {
            // arrege
            int x = 10; int y = 20;
            int expected = 30;
            // action
            int actiont = x + y;
            //assert
            Assert.AreEqual(expected, actiont);
        }*/
    }
}