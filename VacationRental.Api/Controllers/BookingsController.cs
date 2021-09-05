using System.Net;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.ViewModels;
using VacationRental.Services.Contracts;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IBookingService _bookingService;


        public BookingsController(IRentalService rentalService, IBookingService bookingService)
        {
            this._rentalService = rentalService;
            this._bookingService = bookingService;
        }


        /// <summary>
        /// Get Booking by booking Id
        /// </summary>
        /// <param name="bookingId">Booking identifier</param>
        /// <returns>Searched booking</returns>
        [HttpGet]
        [Route("{bookingId:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]

        public IActionResult Get(int bookingId)
        {
            var booking = this._bookingService.Get(bookingId);

            if (booking == null)
                return NotFound("Booking not found");

            return Ok(booking);
        }

        /// <summary>
        /// Creates Booking 
        /// </summary>
        /// <param name="model">Rental identifier, starting date and number of nights.</param>
        /// <returns>Booking identifier</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Post(BookingBindingModel model)
        {
            if (model.Nights <= 0) return BadRequest("Nigts must be positive");

            model.Rental = this._rentalService.Get(model.RentalId);

            if (model.Rental == null) return NotFound("Rental not found");

            BookingViewModel booking = this._bookingService.SaveBooking(model);

            if (booking == null)
                return BadRequest("Not available");

            return Ok(booking.Id);
        }
    }
}
