
using Microsoft.EntityFrameworkCore;

namespace ImageToQR.DB
{

    public class ImageDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public ImageDataContext(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            options.UseSqlite(Configuration.GetConnectionString("BlobConnection"));
        }

        public DbSet<BlobStore> BlobStores { get; set; }
    }
}
