using Data.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PresentationMVC.Data
{
    public class PresantationMVCDbContext : IdentityDbContext
    {
        public PresantationMVCDbContext(DbContextOptions<PresantationMVCDbContext> options) : base(options)
        {
        }
    }
}
