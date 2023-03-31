using Aplication.Services;
using Data.Context;
using Data.Repository;
using Domain.Interfaces;
using Domain.Models;
using Xunit;

namespace UnitTesting
{
    [TestFixture]
    public class Tests
    {

        [Fact]
        public void GetCar() 
        {
            ShopDbContext  shopDbContext = new ShopDbContext(options: new Microsoft.EntityFrameworkCore.DbContextOptions<ShopDbContext>());
            ICarRepository carRepository = new CarRepository(shopDbContext);
            CarServices carServices = new CarServices(carRepository);
            int id = 1;
            var temp = shopDbContext.Cars;


            var execute = carServices.GetViewCar();

            Assert.Equals(temp, shopDbContext);

        }

/*        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }*/
    }
}