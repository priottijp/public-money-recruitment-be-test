using System.Collections.Generic;
using VacationRental.DomainEntities;

namespace VacationRental.DataAccess.Contracts
{
    public interface IBookingRepository : IRepositoryBase<Booking>
    {
        IEnumerable<Booking> GetByRentalId(int rentalId);
    }
}
