using FluentAssertions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class UpdateRentalTests
    {
        private readonly HttpClient _client;

        public UpdateRentalTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task Update_Rental_PreparationDays_Should_Success()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 1,
                PreparationTimeInDays = 1
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
                Start = new DateTime(2021, 10, 01),
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
                Start = new DateTime(2021, 10, 04)
            };

            int postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<int>();
            }


            var updateRentalRequest = new RentalBindingModel
            {
                Units = 1,
                PreparationTimeInDays = 2
            };

            using (var updateRentalResponse = await _client.PutAsJsonAsync($"/api/v1/rentals/{postRentalResult}", updateRentalRequest))
            {
                Assert.True(updateRentalResponse.IsSuccessStatusCode);
            }
        }

        [Fact]
        public async Task Update_Rental_PreparationDays_Should_Fail_Due_To_Overlapping()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 1,
                PreparationTimeInDays = 1
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
                Start = new DateTime(2021, 11, 01)
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
                Start = new DateTime(2021, 11, 04)
            };

            int postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadAsAsync<int>();
            }


            var updateRentalRequest = new RentalBindingModel
            {
                Units = 1,
                PreparationTimeInDays = 3
            };

            using (var updateRentalResponse = await _client.PutAsJsonAsync($"/api/v1/rentals/{postRentalResult}", updateRentalRequest))
            {
                updateRentalResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}