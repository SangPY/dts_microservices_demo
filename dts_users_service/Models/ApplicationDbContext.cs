using Microsoft.EntityFrameworkCore;

namespace dts_users_service.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Students> Students { get; set; }
    }
}
