using efcore_bulk_insert_example.Models;
using Microsoft.EntityFrameworkCore;
namespace efcore_bulk_insert_example.Context
{
    public class BulkExampleContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public BulkExampleContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
    }
}