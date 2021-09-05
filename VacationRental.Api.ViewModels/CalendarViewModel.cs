using System.Collections.Generic;

namespace VacationRental.Api.ViewModels
{
    public class CalendarViewModel
    {
        /// <summary>
        /// Rental identifier
        /// </summary>
        /// <example>2</example>
        public int RentalId { get; set; }

        /// <summary>
        /// Booked dates
        /// </summary>
        public List<CalendarDateViewModel> Dates { get; set; }

    }
}
