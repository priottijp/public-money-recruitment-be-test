using AutoMapper;
using VacationRental.Api.ViewModels;
using VacationRental.DomainEntities;

namespace VacationRental.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Booking, BookingViewModel>().ReverseMap();
            CreateMap<Rental, RentalViewModel>().ReverseMap();
        }
    }
}
