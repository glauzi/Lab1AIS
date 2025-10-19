using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookManager.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManager.DataAccessLayer
{
    internal class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookManagerDB;Trusted_Connection=true;");
        }
    }
}
