using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskSchedular;

namespace HotelBooking.Core.Tasks
{
	public class InvalidateExpiredBookings : IScheduledTask
	{
		public string Schedule => "* 0 0 0 0 0";

		private IRepository<Booking> bookingRepository;

		public InvalidateExpiredBookings(IRepository<Booking> bookingRepository)
		{
			this.bookingRepository = bookingRepository;
		}
			
		public async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			var expiredBookings = bookingRepository.GetAll().Where(x => x.StartDate <= DateTime.Now && x.IsActive == true && x.IsCheckedIn == false);

			foreach(var booking in expiredBookings)
			{
				booking.IsActive = false;
				bookingRepository.Edit(booking);
			}
		}
	}
}
