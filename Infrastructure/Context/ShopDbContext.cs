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
        public DbSet<User> ShopUsers { get; set; }
    }
}
