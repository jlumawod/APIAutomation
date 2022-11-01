using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project2.DataModels;
using RestSharp;
using System.Diagnostics.Contracts;
using System.Net;
using Project2.Helpers;

namespace Project2.Tests
{
    public class BaseTest
    {        
        public readonly List<BookingModel> cleanUpList = new List<BookingModel>();

        [TestInitialize]
        public void TestInitialize()
        {
            //restClient = new RestClient();

            //restClient.AddDefaultHeader("Accept", "application/json");
        }

        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteBooking = await BookingHelper.DeleteBookingById(data.BookingId);
                Assert.AreEqual(deleteBooking.StatusCode, HttpStatusCode.Created, "Status Code does not match");
            }
        }


    }
}
