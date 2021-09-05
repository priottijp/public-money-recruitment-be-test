using System;
using System.Collections.Generic;

namespace VacationRental.Api.ViewModels
{
    public class CalendarDateViewModel
    {
        public CalendarDateViewModel()
        {
            this.Bookings = new List<CalendarBookingViewModel>();
            this.PreparationTimes = new List<PreparationTimeViewModel>();
        }
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Existing Bookings 
        /// </summary>
        public List<CalendarBookingViewModel> Bookings { get; set; }

        /// <summary>
        /// Preparation time ocupations
        /// </summary>
        public List<PreparationTimeViewModel> PreparationTimes { get; set; }
    }
}
