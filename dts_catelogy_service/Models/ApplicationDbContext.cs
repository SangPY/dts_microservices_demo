using Microsoft.EntityFrameworkCore;

namespace dts_catelogy_service.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cataloge> Catalog { get; set;  }
    }
}
