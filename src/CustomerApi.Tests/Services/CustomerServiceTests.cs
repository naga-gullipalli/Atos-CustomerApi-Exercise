using CustomerApi.Domain.Models;
using CustomerApi.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CustomerApi.Tests.Services
{
    public class CustomerServiceTests
    {
        private CustomerService CreateService() => new CustomerService();

        [Fact]
        public void Add_ValidCustomer_ShouldBeAdded()
        {
            // Arrange
            var service = CreateService();
            var customer = new Customer { Firstname = "John", Surname = "Doe" };

            // Act
            service.Add(customer);

            // Assert
            var allCustomers = service.GetAll().ToList();
            Assert.Single(allCustomers);
            Assert.Equal(customer, allCustomers.First());
            Assert.True(customer.Id > 0); // Id is auto-incremented
        }

        [Fact]
        public void Add_NullCustomer_ShouldThrowArgumentNullException()
        {
            // Arrange
            var service = CreateService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.Add(null));
        }

        [Fact]
        public void Add_DuplicateCustomer_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var service = CreateService();
            var customer = new Customer { Firstname = "John", Surname = "Doe" };

            // Add the customer for the first time
            service.Add(customer);

            // Act & Assert: adding the same customer instance again should fail
            var ex = Assert.Throws<InvalidOperationException>(() => service.Add(customer));
            Assert.Equal($"A customer with Id {customer.Id} already exists.", ex.Message);
        }

        [Fact]
        public void Remove_ExistingCustomer_ShouldReturnTrue()
        {
            // Arrange
            var service = CreateService();
            var customer = new Customer { Firstname = "John", Surname = "Doe" };
            service.Add(customer);

            // Act
            var result = service.Remove(customer.Id);

            // Assert
            Assert.True(result);
            Assert.Empty(service.GetAll());
        }

        [Fact]
        public void Remove_NonExistingCustomer_ShouldReturnFalse()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = service.Remove(999); // some ID that doesn't exist

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetAll_WhenCalled_ShouldReturnAllCustomers()
        {
            // Arrange
            var service = CreateService();
            var customer1 = new Customer { Firstname = "John", Surname = "Doe" };
            var customer2 = new Customer { Firstname = "Jane", Surname = "Smith" };
            service.Add(customer1);
            service.Add(customer2);

            // Act
            var allCustomers = service.GetAll().ToList();

            // Assert
            Assert.Equal(2, allCustomers.Count);
            Assert.Contains(customer1, allCustomers);
            Assert.Contains(customer2, allCustomers);
        }

        [Fact]
        public void Customer_Ids_ShouldBeUnique_AutoIncremented()
        {
            // Arrange
            var customer1 = new Customer { Firstname = "John", Surname = "Doe" };
            var customer2 = new Customer { Firstname = "Jane", Surname = "Smith" };

            // Act & Assert
            Assert.NotEqual(customer1.Id, customer2.Id);
            Assert.True(customer2.Id > customer1.Id);
        }
    }
}
