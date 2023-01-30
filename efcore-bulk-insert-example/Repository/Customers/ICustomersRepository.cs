using efcore_bulk_insert_example.Models;

namespace efcore_bulk_insert_example.Repository.Customers
{
    public interface ICustomersRepository
    {
        Task BulkInsertAsync(List<Customer> customers);
        Task AddRangeAsync(List<Customer> customers);
    }
}
