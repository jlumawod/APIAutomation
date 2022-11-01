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
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Project1.Tests
{
    public class BaseTest
    {
        public HttpClient httpClient { get; set; }

        public readonly List<BookingModel> cleanUpList = new List<BookingModel>();

        [TestInitialize]
        public async Task Initialize()
        {
            //Initialize http client
            httpClient = new HttpClient();

            //Authenticate user and get token
            //await BookingHelper.AuthenticateUser(httpClient);

            //Set header
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");            
            
        }

        [TestCleanup]
        public void CleanUp()
        {
            

        }

    }
}
