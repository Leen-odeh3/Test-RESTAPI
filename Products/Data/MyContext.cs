using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class MyContext: DbContext
    {

        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

    }
}
