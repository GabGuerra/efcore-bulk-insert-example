using efcore_bulk_insert_example.Repository.Customers;

namespace efcore_bulk_insert_example.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomersRepository, CustomersRepository>();
        }
    }
}
