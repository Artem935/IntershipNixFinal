using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data.Context
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
            /*Database.EnsureCreated();*/
            /*Database.EnsureDeleted();*/
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Liked> LikedCar { get; set; }
    }
}
