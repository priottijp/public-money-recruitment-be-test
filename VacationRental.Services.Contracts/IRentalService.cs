using VacationRental.Api.ViewModels;

namespace VacationRental.Services.Contracts
{
    public interface IRentalService
    {
        RentalViewModel Get(int RentalId);
        int SaveRental(RentalBindingModel model);
        bool UpdateRental(int rentalId, RentalBindingModel model);
    }
}
