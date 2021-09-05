using System.Net;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.ViewModels;
using VacationRental.Services.Contracts;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IBookingService _bookingService;

        public RentalsController(IRentalService rentalService, IBookingService bookingService)
        {
            this._rentalService = rentalService;
            this._bookingService = bookingService;
        }

        /// <summary>
        /// Get Rental by Rental Id
        /// </summary>
        /// <param name="rentalId">Rental identifier</param>
        /// <returns>Searched Rental</returns>
        [HttpGet]
        [Route("{rentalId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get(int rentalId)
        {
            var rental = this._rentalService.Get(rentalId);

            if (rental == null)
                return NotFound("Rental not found");

            return Ok(rental);
        }

        /// <summary>
        /// Updates an existing Rental
        /// </summary>
        /// <param name="rentalId">Rental identifier</param>
        /// <param name="model">Available units and extra preparation days</param>
        /// <returns>Updated Rental</returns>
        [HttpPut]
        [Route("{rentalId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Put(int rentalId, RentalBindingModel model)
        {
            var rental = this._rentalService.Get(rentalId);

            if (rental == null)
                return NotFound("Rental not found");

            return Ok(rental);

        }

        /// <summary>
        /// Creates a new Rental
        /// </summary>
        /// <param name="model">Available units and extra preparation days</param>
        /// <returns>Created Rental</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post(RentalBindingModel model)
        {
            return Ok(this._rentalService.SaveRental(model));
        }
    }
}
