using System;
using System.Runtime.InteropServices;
using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using Xunit;

namespace HotelBooking.UnitTests
{
    public class BookingManagerTests
    {
        private IBookingManager bookingManager;

        public BookingManagerTests(){
            DateTime start = DateTime.Today.AddDays(10);
            DateTime end = DateTime.Today.AddDays(20);
            IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
            IRepository<Room> roomRepository = new FakeRoomRepository();
            bookingManager = new BookingManager(bookingRepository, roomRepository);
        }

        [Fact]
        public void FindAvailableRoom_StartDateNotInTheFuture_ThrowsArgumentException()
        {
            DateTime date = DateTime.Today;
            Assert.Throws<ArgumentException>(() => bookingManager.FindAvailableRoom(date, date));
        }
        
        [Theory]
        [InlineData(10, 12)]
        [InlineData(10, 15)]
        [InlineData(12, 15)]
        [InlineData(18, 20)]
        [InlineData(10,20)]
        public void FindAvailableroom_RoomNotAvailable_RoomIdEqualMinusOne(int daysInFutureStart, int daysInFutureEnd)
        {
            DateTime date = DateTime.Today.AddDays(daysInFutureStart);
            DateTime date2 = DateTime.Today.AddDays(daysInFutureEnd);
            
            var roomId = bookingManager.FindAvailableRoom(date, date2);
            Assert.Equal(-1, roomId);
        }
        
        [Fact]
        public void FindAvailableRoom_RoomAvailable_RoomIdNotMinusOne()
        {
            // Arrange
            DateTime date = DateTime.Today.AddDays(1);
            // Act
            int roomId = bookingManager.FindAvailableRoom(date, date);
            // Assert
            Assert.NotEqual(-1, roomId);
        }
        [Fact]
        public void FindAvailableRoom_DateInThePast_ThrowsArgumentException()
        {
            DateTime startDate = DateTime.Today.AddDays(-1);
            DateTime endDate = DateTime.Today.AddDays(5);
         
            Assert.Throws<ArgumentException>(() => bookingManager.FindAvailableRoom(startDate, endDate));
            
        }
    }
}
