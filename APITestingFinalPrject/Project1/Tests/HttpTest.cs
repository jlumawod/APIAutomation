using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Tests.TestData;
using Project1.Resources;
using Project1.Helpers;
using Project1.DataModels;
using System.Net;
using System.Net.Http;

namespace Project1.Tests
{
    [TestClass]
    public class HttpTest : BaseTest
    {


        [TestMethod]
        public async Task CreateBooking()
        {
            #region POST Request for new booking
            
            //Send Post request for creating a new booking
            var response = await BookingHelper.CreateNewBooking(httpClient);

            //Deserialize response data
            var postBookingData = JsonConvert.DeserializeObject<BookingModel>(response.Content.ReadAsStringAsync().Result);

            //Assert the status code is OK
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK,"Response Failed");
            #endregion

            #region GET Request for retrieving booking using id of new created booking
            //Send a GET request using booking id created
            var newResponse = await BookingHelper.GetBookingById(httpClient, postBookingData.Bookingid);
            
            //Deserialize response data
            var getBookingData = JsonConvert.DeserializeObject<BookingDetails>(newResponse.Content.ReadAsStringAsync().Result);
            #endregion

            #region Compare test data to created data
            //Assert
            var testData = BookingData.GenerateBookingData();
            Assert.IsNotNull(getBookingData);
            Assert.AreEqual(testData.Firstname, getBookingData.Firstname,"Firstname do not match");
            Assert.AreEqual(testData.Lastname,getBookingData.Lastname,"Lastname do not match");
            Assert.AreEqual(testData.Depositpaid, getBookingData.Depositpaid, "Depositpaid do not match");
            Assert.AreEqual(testData.Additionalneeds, getBookingData.Additionalneeds, "Additionalneeds do not match");
            Assert.AreEqual(testData.Totalprice, getBookingData.Totalprice, "Totalprice do not match");
            Assert.AreEqual(testData.Bookingdates.Checkin, getBookingData.Bookingdates.Checkin.AddDays(1), "Bookingdates do not match");
            Assert.AreEqual(testData.Bookingdates.Checkout, getBookingData.Bookingdates.Checkout.AddDays(1), "Bookingdates do not match");
            #endregion

            #region Add data to cleanuplist
            //Add data to cleanup list
            cleanUpList.Add(postBookingData);
            #endregion


        }
    }
}
