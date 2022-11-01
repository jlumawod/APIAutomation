using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project1.DataModels;


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
