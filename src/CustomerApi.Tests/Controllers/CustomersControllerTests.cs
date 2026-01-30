using CustomerApi.Controllers;
using CustomerApi.Domain.Models;
using CustomerApi.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CustomerApi.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private CustomersController CreateController()
        {
            var service = new CustomerService();
            var logger = new NullLogger<CustomersController>();
            return new CustomersController(service, logger);
        }

        [Fact]
        public void AddCustomer_ReturnsCreated()
        {
            var controller = CreateController();

            var result = controller.Add(new Customer
            {
                Firstname = "John",
                Surname = "Doe"
            });

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            var controller = CreateController();
            var result = controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RemoveCustomer_ReturnsNoContent()
        {
            var controller = CreateController();
            var customer = new Customer { Firstname = "Jane", Surname = "Smith" };
            controller.Add(customer);

            var result = controller.Remove(customer.Id);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void RemoveCustomer_WithInvalidId_ReturnsBadRequest()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Remove(0);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }


        [Fact]
        public void RemoveCustomer_WhenCustomerDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var controller = CreateController();

            // Act
            var result = controller.Remove(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
