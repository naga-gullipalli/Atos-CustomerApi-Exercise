using CustomerApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        void Add(Customer customer);
        bool Remove(int id);
    }
}
