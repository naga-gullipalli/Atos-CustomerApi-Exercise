using CustomerApi.Domain.Models;
using CustomerApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(
        ICustomerService service,
        ILogger<CustomersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Listing all customers");
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            _logger.LogInformation("Adding customer with ID {Id}", customer.Id);
            _service.Add(customer);
            return CreatedAtAction(nameof(GetAll), customer);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Attempted to remove customer with invalid ID: {Id}", id);
                return BadRequest(new { message = "Id must be greater than 0." });
            }

            _logger.LogInformation("Removing customer with ID {Id}", id);

            var removed = _service.Remove(id);
            if (!removed)
                return NotFound();

            return NoContent();
        }
    }
}
