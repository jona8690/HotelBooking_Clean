using System.Collections.Generic;
using HotelBooking.Core;
using Microsoft.AspNetCore.Mvc;


namespace HotelBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly IRepository<Customer> repository;

        public CustomersController(IRepository<Customer> repos)
        {
            repository = repos;
        }

        // GET: api/customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return repository.GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Post(Customer input)
        {
            repository.Add(input);
            return Ok();
        }
    }
}