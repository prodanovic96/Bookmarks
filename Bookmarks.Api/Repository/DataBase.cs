using Bookmarks.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookmarks.Api.Repository
{
    public class DataBase : DbContext
    {        
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
        }

        public DbSet<UrlItem> UrlItems { get; set; }
        public DbSet<UrlList> UrlLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlItem>().ToTable("UrlItem");
            modelBuilder.Entity<UrlList>().ToTable("UrlList"); 
        }
    }
}
