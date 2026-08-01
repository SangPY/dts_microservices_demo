using Microsoft.EntityFrameworkCore;

namespace dts_orders_service.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Orders> Orders { get; set; }
    }
}
