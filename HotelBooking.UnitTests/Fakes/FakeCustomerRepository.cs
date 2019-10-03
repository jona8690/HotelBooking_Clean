using System;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Core;

namespace HotelBooking.UnitTests.Fakes
{
    public class FakeCustomerRepository : IRepository<Customer>
    {
        private readonly HotelBookingContext db;

        public FakeCustomerRepository(HotelBookingContext context)
        {
            db = context;
        }

        public CustomerRepository(HotelBookingContext context)
        {
            db = context;
        }

        // This field is exposed so that a unit test can validate that the
        // Add method was invoked.
        public bool addWasCalled = false;

        public void Add(Customer entity)
        {
            addWasCalled = true;
        }

        // This field is exposed so that a unit test can validate that the
        // Edit method was invoked.
        public bool editWasCalled = false;

        public void Edit(Customer entity)
        {
            editWasCalled = true;
        }

        public Customer Get(int id)
        {
            return new Customer { Id = 2, Name = "Jeppe", Email = "coolemail2@dk.dk" };
        }

        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>
            {
               new Customer { Id=1, Name="Henrik", Email="coolemail1@dk.dk"},
                new Customer { Id=2, Name="Jeppe", Email="coolemail2@dk.dk" },
                new Customer { Id=3, Name="Bent", Email="coolemail3@dk.dk" },
            };
            return customers;
        }

        // This field is exposed so that a unit test can validate that the
        // Remove method was invoked.
        public bool removeWasCalled = false;

        public void Remove(int id)
        {
            removeWasCalled = true;
        }
    }
}
