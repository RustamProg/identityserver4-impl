using Microsoft.EntityFrameworkCore;

namespace IdentityServer4_implementation.Models
{
    public class SqlDbContext: DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options): base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
    }
}