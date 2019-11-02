using System;
using TechTalk.SpecFlow;

namespace HotelBooking.UnitTests
{
    [Binding]
    public class BookingSteps
    {
        int result;
        //HotelBooking.Core.BookingManager bookingManager = new HotelBooking.Core.BookingManager();
        [Given(@"I have entered an valid Start date")]
        public void GivenIHaveEnteredAnValidStartDate(DateTime dateTime)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have entered an valid End date")]
        public void GivenIHaveEnteredAnValidEndDate(DateTime dateTime)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I press book")]
        public void WhenIPressBook()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be room has been booked on the screen")]
        public void ThenTheResultShouldBeRoomHasBeenBookedOnTheScreen()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
