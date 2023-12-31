﻿
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

        //public void DeleteImage(Guid uid)
        //{
        //    var del = this.BlobStores.Find(uid);
        //    this.BlobStores.Remove(del);
        //    this.SaveChanges();
        //}

        public DbSet<BlobStore> BlobStores { get; set; }
    }
}
