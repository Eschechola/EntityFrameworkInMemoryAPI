using EntityFrameworkInMemoryAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkInMemoryAPI.Data.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {}

        public DbSet<Customer> Customers { get; set; }
    }
}
