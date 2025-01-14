﻿using System.Collections.Generic;
using HotelBooking.Core;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                repository.Remove(id);
                return Ok();
            }
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer input)
        {
            input.Id = id;
            repository.Edit(input);
            return Ok();
        }
    }
}