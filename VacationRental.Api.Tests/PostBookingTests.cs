﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Tests
{
    [Collection("Integration")]
    public class PostBookingTests
    {
        private readonly HttpClient _client;

        public PostBookingTests(IntegrationFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 4
            };

            int postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadAsAsync<int>();
            }

            var postBookingRequest = new BookingBindingModel
            {
                RentalId = postRentalResult,
                Nights = 3,
                Start = new DateTime(2001, 01, 01)
            };

            int postBookingResult;
            using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest))
            {
                Assert.True(postBookingResponse.IsSuccessStatusCode);
                postBookingResult = await postBookingResponse.Content.ReadAsAsync<int>();
            }

            using (var getBookingResponse = await _client.GetAsync($"/api/v1/bookings/{postBookingResult}"))
            {
                Assert.True(getBookingResponse.IsSuccessStatusCode);

                var getBookingResult = await getBookingResponse.Content.ReadAsAsync<BookingViewModel>();
                Assert.Equal(postBookingRequest.RentalId, getBookingResult.RentalId);
                Assert.Equal(postBookingRequest.Nights, getBookingResult.Nights);
                Assert.Equal(postBookingRequest.Start, getBookingResult.Start);
            }
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 1
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
                Nights = 3,
                Start = new DateTime(2002, 01, 01)
            };

            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
            }

            var postBooking2Request = new BookingBindingModel
            {
                RentalId = postRentalResult,
                Nights = 1,
                Start = new DateTime(2002, 01, 02)
            };

            var result = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request);

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await result.Content.ReadAsStringAsync();
            error.Should().NotBeNullOrWhiteSpace();
            error.Should().Contain("Not available");
        }
    }
}
