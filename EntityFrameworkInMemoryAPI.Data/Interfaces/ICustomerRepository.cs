using System.Threading.Tasks;
using System.Collections.Generic;
using EntityFrameworkInMemoryAPI.Data.Entities;

namespace EntityFrameworkInMemoryAPI.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> Insert(Customer customer);
        Task<Customer> Update(Customer customer);
        Task<Customer> Get(int id);
        Task<List<Customer>> Get();
        Task Delete(int id);
    }
}
