using System;
using System.Collections;
using System.Collections.Generic;

namespace HotelBooking.UnitTests
{
    public class FullyBookedDatesGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data  = new List<object[]>
        {
            new object[] { DateTime.Today.AddDays(8), DateTime.Today.AddDays(10) },
            new object[] { DateTime.Today.AddDays(10), DateTime.Today.AddDays(12) },
            new object[] { DateTime.Today.AddDays(13), DateTime.Today.AddDays(15) },
            new object[] { DateTime.Today.AddDays(17), DateTime.Today.AddDays(20) },
            new object[] { DateTime.Today.AddDays(19), DateTime.Today.AddDays(25) },
            new object[] { DateTime.Today.AddDays(20), DateTime.Today.AddDays(25) }
        };
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}