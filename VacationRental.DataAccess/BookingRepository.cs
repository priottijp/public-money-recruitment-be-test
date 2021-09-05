using System.Collections.Generic;
using System.Linq;
using VacationRental.DataAccess.Contracts;
using VacationRental.DomainEntities;

namespace VacationRental.DataAccess
{
    public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
    {
        public BookingRepository(VacationApiContext context) : base(context) { }

        public IEnumerable<Booking> GetByRentalId(int rentalId)
            => base.GetAll().Where(x => x.RentalId == rentalId);
    }

}
