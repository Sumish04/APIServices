using APIServices.Context.CV_Storage;
using APIServices.Entities.BLOB_Storage;
using APIServices.Entities.CV_Storage;
using Microsoft.EntityFrameworkCore;

namespace APIServices.Context.BLOB_Storage
{
    public class BlobDbContext : DbContext
    {
        public BlobDbContext(DbContextOptions<BlobDbContext> options) : base(options) { }

        public DbSet<Documents> Documents { get; set; }
    }
}
