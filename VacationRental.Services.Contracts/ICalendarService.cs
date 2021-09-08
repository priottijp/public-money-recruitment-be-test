using System;
using VacationRental.Api.ViewModels;

namespace VacationRental.Services.Contracts
{
    public interface ICalendarService 
    {
        CalendarViewModel Get(RentalViewModel rental, DateTime start, int nights);
    }
}
