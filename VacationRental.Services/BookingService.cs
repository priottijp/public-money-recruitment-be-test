using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Api.ViewModels;
using VacationRental.DataAccess.Contracts;
using VacationRental.DomainEntities;
using VacationRental.Services.Contracts;

namespace VacationRental.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public BookingViewModel Get(int bookingId)
            => this._mapper.Map<BookingViewModel>(_bookingRepository.Get(bookingId));

        public BookingViewModel SaveBooking(BookingBindingModel model)
        {
            var bookings = this._bookingRepository.GetAll();
            List<int> bookedUnits = new List<int>();
            for (var i = 0; i < model.Nights; i++)
            {
                var count = 0;
                foreach (var booking in bookings)
                {
                    int existingBookingDays = booking.Nights + model.Rental.PreparationTimeInDays;
                    int newBookingDays = booking.Nights + model.Rental.PreparationTimeInDays;

                    if (booking.RentalId == model.RentalId
                        && (booking.Start <= model.Start.Date && booking.Start.AddDays(existingBookingDays) > model.Start.Date)
                        || (booking.Start < model.Start.AddDays(newBookingDays) && booking.Start.AddDays(existingBookingDays) >= model.Start.AddDays(newBookingDays))
                        || (booking.Start > model.Start && booking.Start.AddDays(existingBookingDays) < model.Start.AddDays(newBookingDays)))
                    {
                        count++;
                        bookedUnits.Add(booking.Unit);
                    }
                }
                if (count >= model.Rental.Units) return null;
            }

            var resource = new ResourceIdViewModel { Id = bookings.Count() + 1 };

            Booking newBooking = new Booking()
            {
                Id = resource.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start.Date,
                Unit = this.GetAvailableUnit(model.Rental.Units, bookedUnits)
            };

            _bookingRepository.Insert(newBooking);

            return _mapper.Map<BookingViewModel>(newBooking);
        }
        private int GetAvailableUnit(int units, List<int> bookedUnits)
        {
            int unit = 0;

            for (int i = 1; i <= units; i++)
                if (!bookedUnits.Contains(i)) { unit = i; break; }

            return unit;
        }
    }
}
