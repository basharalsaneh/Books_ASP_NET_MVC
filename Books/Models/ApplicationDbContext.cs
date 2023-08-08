using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Books.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Book> books { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}