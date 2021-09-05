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
        private readonly IMapper _mapper;
        public RentalService(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }

        public RentalViewModel Get(int RentalId)
            => this._mapper.Map<RentalViewModel>(this._rentalRepository.Get(RentalId));

        public int SaveRental(RentalBindingModel model)
        {
            var newRentalId = _rentalRepository.GetAll().Count() + 1;

            _rentalRepository.Insert(new Rental { Id = newRentalId, Units = model.Units, PreparationTimeInDays = model.PreparationTimeInDays });

            return newRentalId;
        }
    }
}
