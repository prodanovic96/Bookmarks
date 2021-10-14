using Bookmarks.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

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
