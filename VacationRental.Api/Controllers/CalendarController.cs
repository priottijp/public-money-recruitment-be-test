using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Services.Contracts;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ICalendarService _calendarService;

        public CalendarController(IRentalService rentalService, ICalendarService calendarService)
        {
            _rentalService = rentalService;
            _calendarService = calendarService;
        }

        /// <summary>
        /// Retrieves the availability of an existing rental
        /// </summary>
        /// <param name="rentalId">Rental identifier</param>
        /// <param name="start">Starting date</param>
        /// <param name="nights">Number of nights.</param>
        /// <returns>Created calendar entrance</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                return BadRequest("Nights must be positive");

            var rental = _rentalService.Get(rentalId);

            if (rental == null)
                return NotFound("Rental not found");

            var result = this._calendarService.Get(rental, start, nights);

            return Ok(result);
        }
    }
}
