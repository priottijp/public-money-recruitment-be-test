using System;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class GetCalendarTests
    {
        private readonly HttpClient _client;

        public GetCalendarTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenGetCalendar_ThenAGetReturnsTheCalculatedCalendar()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 2,
                PreparationTimeInDays = 2
            };

            int postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<int>();
            }

            var postBooking1Request = new BookingBindingModel
            {
                RentalId = postRentalResult,
                Nights = 1,
                Start = new DateTime(2021, 09, 01)
            };

            int postBooking1Result;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                postBooking1Result = await postBooking1Response.Content.ReadAsAsync<int>();
            }

            var postBooking2Request = new BookingBindingModel
            {
                RentalId = postRentalResult,
                Nights = 2,
                Start = new DateTime(2021, 09, 02)
            };

            int postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<int>();
            }

            var postBooking3Request = new BookingBindingModel
            {
                RentalId = postRentalResult,
                Nights = 2,
                Start = new DateTime(2021, 09, 05)
            };

            int postBooking3Result;
            using (var postBooking3Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking3Request))
            {
                Assert.True(postBooking3Response.IsSuccessStatusCode);
                postBooking3Result = await postBooking3Response.Content.ReadAsAsync<int>();
            }

            using (var getCalendarResponse = await _client.GetAsync($"/api/v1/calendar?rentalId={postRentalResult}&start=2021-09-01&nights=10"))
            {
                Assert.True(getCalendarResponse.IsSuccessStatusCode);

                var getCalendarResult = await getCalendarResponse.Content.ReadAsAsync<CalendarViewModel>();

                Assert.Equal(postRentalResult, getCalendarResult.RentalId);
                Assert.Equal(10, getCalendarResult.Dates.Count);

                Assert.Equal(new DateTime(2021, 09, 01), getCalendarResult.Dates[0].Date);
                Assert.Single(getCalendarResult.Dates[0].Bookings);
                Assert.Contains(getCalendarResult.Dates[0].Bookings, x => x.Id == postBooking1Result);

                Assert.Equal(new DateTime(2021, 09, 02), getCalendarResult.Dates[1].Date);
                Assert.Single(getCalendarResult.Dates[1].Bookings);
                Assert.Single(getCalendarResult.Dates[1].PreparationTimes);
                Assert.Contains(getCalendarResult.Dates[1].Bookings, x => x.Id == postBooking2Result);

                Assert.Equal(new DateTime(2021, 09, 03), getCalendarResult.Dates[2].Date);
                Assert.Single(getCalendarResult.Dates[2].Bookings);
                Assert.Single(getCalendarResult.Dates[2].PreparationTimes);

                Assert.Equal(new DateTime(2021, 09, 04), getCalendarResult.Dates[3].Date);
                Assert.Single(getCalendarResult.Dates[3].PreparationTimes);
            }
        }
    }
}
