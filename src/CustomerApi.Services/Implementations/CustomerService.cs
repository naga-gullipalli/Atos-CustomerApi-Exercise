using CustomerApi.Domain.Models;
using CustomerApi.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ConcurrentDictionary<int, Customer> _customers = new();

        public IEnumerable<Customer> GetAll()
        {
            return _customers.Values.ToList();
        }

        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");

            if (!_customers.TryAdd(customer.Id, customer))
            {
                throw new InvalidOperationException($"A customer with Id {customer.Id} already exists.");
            }
        }

        public bool Remove(int id)
        {
            return _customers.TryRemove(id, out _);
        }
    }
}
