using System;

namespace VacationRental.Api.ViewModels
{
    public class BookingBindingModel
    {
        /// <summary>
        /// Rental Identifier
        /// </summary>
        public int RentalId { get; set; }


        /// <summary>
        /// Starting booking date
        /// </summary>
        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        private DateTime _startIgnoreTime;

        /// <summary>
        /// Number of Nights
        /// </summary>
        public int Nights { get; set; }

        public RentalViewModel Rental { get; set; }

    }
}
