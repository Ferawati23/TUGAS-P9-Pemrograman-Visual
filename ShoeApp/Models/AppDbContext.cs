using Microsoft.EntityFrameworkCore;

namespace ShoeApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shoe> Shoes { get; set; }
    }
}
