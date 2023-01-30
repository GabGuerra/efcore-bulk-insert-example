using EFCore.BulkExtensions;
using efcore_bulk_insert_example.Context;
using efcore_bulk_insert_example.Models;

namespace efcore_bulk_insert_example.Repository.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        protected BulkExampleContext _context { get; set; }
        public CustomersRepository(BulkExampleContext context) 
        {
            _context = context;
        }

        public async Task BulkInsertAsync(List<Customer> customers)
        {           
            foreach (var item in customers)
            {
                item.Id = Guid.NewGuid();
            }

            await _context.BulkInsertAsync(customers);
        }

        public async Task AddRangeAsync(List<Customer> customers)
        {
            await _context.AddRangeAsync(customers);
            await _context.SaveChangesAsync();
        }
    }
}
