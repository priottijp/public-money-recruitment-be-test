using AutoMapper;
using System.Linq;
using VacationRental.Api.ViewModels;
using VacationRental.DataAccess.Contracts;
using VacationRental.DomainEntities;
using VacationRental.Services.Contracts;

namespace VacationRental.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public RentalService(IBookingService bookingService, IRentalRepository rentalRepository, IMapper mapper, IBookingRepository bookingRepository)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _bookingService = bookingService;
        }

        public RentalViewModel Get(int RentalId)
            => this._mapper.Map<RentalViewModel>(this._rentalRepository.Get(RentalId));

        public int SaveRental(RentalBindingModel model)
        {
            var newRentalId = _rentalRepository.GetAll().Count() + 1;

            _rentalRepository.Insert(new Rental { Id = newRentalId, Units = model.Units, PreparationTimeInDays = model.PreparationTimeInDays });

            return newRentalId;
        }

        public bool UpdateRental(int rentalId, RentalBindingModel model)
        {

            var bookings = _bookingRepository.GetByRentalId(rentalId).ToList();

            var auxRental = new Rental() { Id = _rentalRepository.GetAll().Count() + 1, Units = model.Units, PreparationTimeInDays = model.PreparationTimeInDays };

            _rentalRepository.Insert(auxRental);

            bool canUpdate = true;

            foreach (var b in bookings)
            {
                var bbm = new BookingBindingModel() { RentalId = rentalId, Nights = b.Nights, Start = b.Start };
                bbm.SetRental(_mapper.Map<RentalViewModel>(auxRental));

                var result = _bookingService.SaveBooking(bbm);
                if (result == null)
                {
                    canUpdate = false;
                    break;
                }
            }

            var newBookings = _bookingRepository.GetByRentalId(auxRental.Id);
            var rental = _rentalRepository.Get(rentalId);
            if (canUpdate)
            {
                foreach (var b in bookings) _bookingRepository.Delete(b);

                foreach (var nb in newBookings)
                {
                    nb.RentalId = rentalId;
                    _bookingRepository.Update(nb);
                }

                rental.PreparationTimeInDays = model.PreparationTimeInDays;
                _rentalRepository.Update(rental);
            }
            else
            {
                foreach (var nb in newBookings)
                    _bookingRepository.Delete(nb);
            }
            _rentalRepository.Delete(_rentalRepository.Get(auxRental.Id));

            return canUpdate;
        }
    }
}
