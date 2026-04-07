using Microsoft.EntityFrameworkCore;
using PruebaTecnicaRossmonApi.Entities;

namespace PruebaTecnicaRossmonApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
 