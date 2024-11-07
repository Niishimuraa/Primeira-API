using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : 
            base(options) {  }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
