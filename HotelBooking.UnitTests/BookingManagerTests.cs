using System;
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

        [Fact]
        public void FindAvailableRoom_RoomNotAvailableStart_RoomIdEqualMinusOne()
        {
            DateTime date = DateTime.Today.AddDays(10);
            var roomId = bookingManager.FindAvailableRoom(date, date);
            Assert.Equal(-1, roomId);
        }
        
        [Fact]
        public void FindAvailableRoom_RoomNotAvailableEnd_RoomIdEqualMinusOne()
        {
            DateTime date = DateTime.Today.AddDays(20);
            var roomId = bookingManager.FindAvailableRoom(date, date);
            Assert.Equal(-1, roomId);
        }
        
        [Fact]
        public void FindAvailableRoom_RoomNotAvailableMiddle_RoomIdEqualMinusOne()
        {
            DateTime date = DateTime.Today.AddDays(15);
            var roomId = bookingManager.FindAvailableRoom(date, date);
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

    }
}
