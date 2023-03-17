using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
