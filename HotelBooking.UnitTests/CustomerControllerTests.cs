using System;
using System.Collections.Generic;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using HotelBooking.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class CustomersControllerTests
    {
        private CustomersController controller;
        private Mock<IRepository<Customer>> fakeCustomerRepository;

        public CustomersControllerTests()
        {
            var customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "Henrik", Email = "coolemail1@dk.dk"},
                new Customer {Id = 2, Name = "Jeppe", Email = "coolemail2@dk.dk"},
                new Customer {Id = 3, Name = "Bent", Email = "coolemail3@dk.dk"}
            };

            // Create fake CustomerRepository. 
            fakeCustomerRepository = new Mock<IRepository<Customer>>();

            // Implement fake GetAll() method.
            fakeCustomerRepository.Setup(x => x.GetAll()).Returns(customers);


            // Implement fake Get() method.
            //fakeCustomerRepository.Setup(x => x.Get(2)).Returns(rooms[1]);


            // Alternative setup with argument matchers:

            // Any integer:
            //customerRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(rooms[1]);

            // Integers from 1 to 2 (using a predicate)
            // If the fake Get is called with an another argument value than 1 or 2,
            // it returns null, which corresponds to the behavior of the real
            // repository's Get method.
            fakeCustomerRepository.Setup(x => x.Get(It.Is<int>(id => id > 0 && id < 3))).Returns(customers[1]);

            // Integers from 1 to 2 (using a range)
            //customerRepository.Setup(x => x.Get(It.IsInRange<int>(1, 2, Range.Inclusive))).Returns(rooms[1]);


            // Create CustomersController
            controller = new CustomersController(fakeCustomerRepository.Object);
        }

        [Fact]
        public void GetAll_ReturnsListWithCorrectNumberOfCustomers()
        {
            // Act
            var result = controller.Get() as List<Customer>;
            var noOfCustomers = result.Count;

            // Assert
            Assert.Equal(3, noOfCustomers);
        }

        [Fact]
        public void GetById_RoomExists_ReturnsIActionResultWithCustomer()
        {
            // Act
            var result = controller.Get(3) as ObjectResult;
            var customer = result.Value as Customer;
            var customerId = customer.Id;


            // Assert
            Assert.InRange<int>(customerId, 1, 3);
        }

        [Fact]
        public void Delete_WhenIdIsLargerThanZero_RemoveIsCalled()
        {
            // Act
            controller.Delete(1);

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }

        [Fact]
        public void Delete_WhenIdIsLessThanOne_RemoveIsNotCalled()
        {
            // Act
            controller.Delete(0);

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(It.IsAny<int>()), Times.Never());
        }

        [Fact]
        public void Delete_WhenIdIsLargerThanTwo_RemoveThrowsException()
        {
            // Instruct the fake Remove method to throw an InvalidOperationException, if a customer id that
            // does not exist in the repository is passed as a parameter. This behavior corresponds to
            // the behavior of the real repoository's Remove method.
            fakeCustomerRepository.Setup(x =>
                x.Remove(It.Is<int>(id => id < 1 || id > 2))).Throws<InvalidOperationException>();

            // Assert
            Assert.Throws<InvalidOperationException>(() => controller.Delete(3));

            // Assert against the mock object
            fakeCustomerRepository.Verify(x => x.Remove(It.IsAny<int>()));
        }
    }
}