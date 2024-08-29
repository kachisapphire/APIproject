using APIproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Data
{
    public class MyBookShopContext : DbContext
    {
        public MyBookShopContext(DbContextOptions<MyBookShopContext> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
