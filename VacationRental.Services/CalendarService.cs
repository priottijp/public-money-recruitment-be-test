using System.Collections.Generic;
using VacationRental.Api.ViewModels;
using VacationRental.DataAccess.Contracts;
using VacationRental.Services.Contracts;

namespace VacationRental.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IBookingRepository _bookingRepository;

        public CalendarService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public CalendarViewModel Get(RentalViewModel rental, System.DateTime start, int nights)
        {
            var result = new CalendarViewModel
            {
                RentalId = rental.Id,
                Dates = new List<CalendarDateViewModel>()
            };

            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes = new List<PreparationTimeViewModel>()
                };

                var bookings = _bookingRepository.GetAll();

                foreach (var booking in bookings)
                {
                    if (booking.RentalId == rental.Id && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id, Unit = booking.Unit });

                    if (date.Date >= booking.Start.AddDays(booking.Nights)
                        && date.Date < booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays))
                        date.PreparationTimes.Add(new PreparationTimeViewModel { Unit = booking.Unit });
                }
                result.Dates.Add(date);
            }

            return result;
        }
    }
}
