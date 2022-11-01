using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project1.DataModels;
using Project1.Helpers;
using System.Net;


namespace Project1.Tests
{
    public class BaseTest
    {
        

        public readonly List<BookingModel> cleanUpList = new List<BookingModel>();

        [TestInitialize]
        public async Task Initialize()
        {

        }

        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteBooking = await BookingHelper.DeleteBooking(data.BookingId);
                Assert.AreEqual(deleteBooking.StatusCode, HttpStatusCode.Created, "Status Code does not match");
            }

        }

    }
}
