using Microsoft.EntityFrameworkCore;
using ProjetWebAPI.Models.DTO;

namespace ProjetWebAPI.DAL
{
    public class DbFactoryContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbFactoryContext(DbContextOptions<DbFactoryContext> options)
               : base(options) { }
    }
}
