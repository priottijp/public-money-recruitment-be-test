using VacationRental.Api.ViewModels;

namespace VacationRental.Services.Contracts
{
    public interface IBookingService
    {
        BookingViewModel Get(int bookingId);
        BookingViewModel SaveBooking(BookingBindingModel model);
    }
}
