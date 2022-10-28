using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.DataModels;
using Project1.Resources;
using Project1.Tests.TestData;
using Project1.Helpers;

namespace Project1.Tests
{
    public class BaseTest
    {
        public HttpClient httpClient { get; set; }

        public readonly List<BookingModel> cleanUpList = new List<BookingModel>();

        [TestInitialize]
        public void Initialize()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //await BookingHelper.AuthenticateUser(httpClient);
        }

        [TestCleanup]
        public void CleanUp()
        {
            

        }

    }
}
