using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2.Helpers;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Tests.TestData;
using RestSharp;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace Project2.Tests
{
    [TestClass]
    public class RestTests : BaseTest
    {
        
        [TestMethod]
        public async Task CreateBooking()
        {            
            // Send POST Request to create new booking
            var response = await BookingHelper.PostBooking();
            
            //Send GET Request to retrieve data using booking id created
            var getResponse = await BookingHelper.GetBookingById(response.Data.BookingId);
            var getResponseData = getResponse.Data;

            //Assert Response Data
            var userDetails = BookingData.GenerateBookingData();
            
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(userDetails.Firstname, getResponseData.Firstname, "Firstname does not match");
            Assert.AreEqual(userDetails.Lastname, getResponseData.Lastname, "Lastname does not match");
            Assert.AreEqual(userDetails.Totalprice, getResponseData.Totalprice, "Totalprice does not match");
            Assert.AreEqual(userDetails.Additionalneeds, getResponseData.Additionalneeds, "Additionalneeds does not match");
            Assert.AreEqual(userDetails.Depositpaid, getResponseData.Depositpaid, "Depositpaid does not match");
            Assert.AreEqual(userDetails.Bookingdates.Checkin, getResponseData.Bookingdates.Checkin.AddDays(1), "Checkin does not match");
            Assert.AreEqual(userDetails.Bookingdates.Checkout, getResponseData.Bookingdates.Checkout.AddDays(1), "Checkout does not match");

            cleanUpList.Add(response.Data);
        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            #region Create New Booking
            // Send POST Request to create new booking
            var response = await BookingHelper.PostBooking();

            //Get Data
            var responseData = response.Data;

            //Assert status code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            #endregion

            //Send GET Request to retrieve data using booking id created
            var getResponse = await BookingHelper.GetBookingById(responseData.BookingId);
            var getResponseData = getResponse.Data;

            //Send PUT Request
            var updateBookingDetails = BookingData.UpdatedBookingData();
            var updateResponse = await BookingHelper.UpdateBookingById(updateBookingDetails, responseData.BookingId);
            Assert.AreEqual(updateResponse.StatusCode, HttpStatusCode.OK, "Status Code does not match");

            //Assert Updates
            var updatedBooking = await BookingHelper.GetBookingById( responseData.BookingId);

            Assert.AreEqual(updateBookingDetails.Firstname, updatedBooking.Data.Firstname, "Firstname does not match");
            Assert.AreEqual(updateBookingDetails.Lastname, updatedBooking.Data.Lastname, "Lastname does not match");
            Assert.AreEqual(updateBookingDetails.Totalprice, updatedBooking.Data.Totalprice, "Totalprice does not match");
            Assert.AreEqual(updateBookingDetails.Depositpaid, updatedBooking.Data.Depositpaid, "Depositpaid does not match");
            Assert.AreEqual(updateBookingDetails.Additionalneeds, updatedBooking.Data.Additionalneeds, "Additionalneeds does not match");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkin, updatedBooking.Data.Bookingdates.Checkin.AddDays(1), "Checkin does not match");
            Assert.AreEqual(updateBookingDetails.Bookingdates.Checkout, updatedBooking.Data.Bookingdates.Checkout.AddDays(1), "Checkout does not match");

            cleanUpList.Add(response.Data);
        }

        [TestMethod]
        public async Task DeleteBooking()
        {
            #region Create New Booking
            // Send POST Request to create new booking
            var response = await BookingHelper.PostBooking();

            //Get Data
            var responseData = response.Data;

            //Assert status code
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            #endregion

            var deleteBooking = await BookingHelper.DeleteBookingById(responseData.BookingId);
            Assert.AreEqual(deleteBooking.StatusCode, HttpStatusCode.Created, "Status Code does not match");

        }

        [TestMethod]
        public async Task InvalidBooking()
        {
            var invalidBooking = await BookingHelper.GetBookingById(-2);
            Assert.AreEqual(invalidBooking.StatusCode, HttpStatusCode.NotFound, "Status Code does not match");
        }


    }
}
