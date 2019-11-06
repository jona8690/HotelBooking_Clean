using System;
using TechTalk.SpecFlow;

namespace HotelBooking.UnitTests
{
    [Binding]
    public class BookingSteps
    {
        [Given(@"That I check in (.*) days")]
        public void GivenThatICheckInDays(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"That I check out in (.*)")]
        public void GivenThatICheckOutIn(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"i try to book")]
        public void WhenITryToBook()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"booking manager should return a room id")]
        public void ThenBookingManagerShouldReturnARoomId()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"booking manager should throw Argument Exception")]
        public void ThenBookingManagerShouldThrowArgumentException()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"booking manager should return (.*)")]
        public void ThenBookingManagerShouldReturn(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
