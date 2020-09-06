using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkInMemoryAPI.Data.Context;
using EntityFrameworkInMemoryAPI.Data.Entities;
using EntityFrameworkInMemoryAPI.Data.Interfaces;

namespace EntityFrameworkInMemoryAPI.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApiContext _context;

        public CustomerRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<Customer> Insert(Customer customer)
        {
            try
            {
                await _context.AddAsync(customer);
                await _context.SaveChangesAsync();

                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Customer> Update(Customer customer)
        {
            try
            {
                _context.Entry(customer).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<Customer> Get(int id)
        {
            try
            {
                return await _context.Customers
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Customer>> Get()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task Delete(int id)
        {
            try
            {

                var customer = Get(id);

                _context.Remove(customer);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
