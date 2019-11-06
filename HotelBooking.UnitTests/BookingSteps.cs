using HotelBooking.Core;
using HotelBooking.UnitTests.Fakes;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace HotelBooking.UnitTests
{
    [Binding]
    public class BookingSteps
    {
		private IBookingManager bookingManager;

		private DateTime BookingStart;
		private DateTime BookingEnd;
		private int RoomId;

		private Exception ThrownException;

		public BookingSteps()
		{
			DateTime start = DateTime.Today.AddDays(10);
			DateTime end = DateTime.Today.AddDays(20);
			IRepository<Booking> bookingRepository = new FakeBookingRepository(start, end);
			IRepository<Room> roomRepository = new FakeRoomRepository();
			bookingManager = new BookingManager(bookingRepository, roomRepository);
		}

		[Given(@"That I check in (.*) days")]
        public void GivenThatICheckInDays(int p0)
        {
			this.BookingStart = DateTime.Now.AddDays(p0);
        }
        
        [Given(@"That I check out in (.*)")]
        public void GivenThatICheckOutIn(int p0)
        {
			this.BookingEnd = DateTime.Now.AddDays(p0);
        }
        
        [When(@"i try to book")]
        public void WhenITryToBook()
        {
			try
			{
				this.RoomId = bookingManager.FindAvailableRoom(BookingStart, BookingEnd);
			} catch(Exception e)
			{
				this.ThrownException = e;
			}
		}
        
        [Then(@"booking manager should return a room id")]
        public void ThenBookingManagerShouldReturnARoomId()
        {
			Assert.True(this.RoomId > 0); // A room id has been assigned
        }
        
        [Then(@"booking manager should throw Argument Exception")]
        public void ThenBookingManagerShouldThrowArgumentException()
        {
			Assert.True(this.ThrownException is ArgumentException);
        }
        
        [Then(@"booking manager should return (.*)")]
        public void ThenBookingManagerShouldReturn(int p0)
        {
			Assert.Equal(p0, this.RoomId);
        }
    }
}
