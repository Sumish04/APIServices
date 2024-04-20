using APIServices.Entities.CV_Storage;
using Microsoft.EntityFrameworkCore;

namespace APIServices.Context.CV_Storage
{
    public class CVDbContext : DbContext
    {
        public CVDbContext(DbContextOptions<CVDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
    }
}
