using System;

namespace VacationRental.Api.ViewModels
{
    public class BookingViewModel
    {
        /// <summary>
        /// Booking Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rental identifier
        /// </summary>
        ///<example>1</example>
        public int RentalId { get; set; }

        /// <summary>
        /// Starting day for the booking 
        /// </summary>
        ///<example>01/01/2021</example>
        public DateTime Start { get; set; }

        /// <summary>
        /// Number of nights
        /// </summary>
        ///<example>5</example>
        public int Nights { get; set; }

        /// <summary>
        /// Number of unit
        /// </summary>
        ///<example>4</example>
        public int Unit { get; set; }
    }
}
