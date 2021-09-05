using VacationRental.DataAccess.Contracts;
using VacationRental.DomainEntities;

namespace VacationRental.DataAccess
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(VacationApiContext context) : base(context) { }
    }

}
