using VacationRental.DataAccess.Contracts;
using VacationRental.DomainEntities;

namespace VacationRental.DataAccess
{
    public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
    {
        public RentalRepository(VacationApiContext context) : base(context) { }
    }
}
