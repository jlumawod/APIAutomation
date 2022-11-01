using Project1.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.DataModels;
using Project1.Tests.TestData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace Project1.Helpers
{
    public class BookingHelper
    {

        public static async Task<HttpResponseMessage> CreateNewBooking(HttpClient client)
        {
            //Serialize content
            var request = JsonConvert.SerializeObject(BookingData.GenerateBookingData());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Request
            return await client.PostAsync(Endpoints.PostBooking(), postRequest);
        }


        public static async Task<HttpResponseMessage> GetBookingById(HttpClient client, int id)
        {
            //Send Request
            return await client.GetAsync(Endpoints.GetBookingById(id));            
        }

        public static async Task<HttpResponseMessage> UpdateBookingById(HttpClient client, int id, BookingDetails bookingDetails)
        {
            //Authenticate user and get token
            await BookingHelper.AuthenticateUser(client);

            //Serialize request
            var request = JsonConvert.SerializeObject(bookingDetails);
            var putRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send PUT Request
            return await client.PutAsync(Endpoints.UpdateBookingById(id), putRequest);
        }

        public static async Task<HttpResponseMessage> DeleteBooking(HttpClient client, int id)
        {
            
            //Authenticate user and get token
            await BookingHelper.AuthenticateUser(client);

            //Send DELETE Request
            return await client.DeleteAsync(Endpoints.DeleteBooking(id));            
        }

        private static async Task AuthenticateUser(HttpClient client)
        {
            // Get token
            var token = await BookingHelper.GetToken(client);

            // Set Request Header            
            client.DefaultRequestHeaders.Add("Cookie", $"token={token}");
        }

        private static async Task<string> GetToken(HttpClient client)
        {
            // Serialize Content            
            var request = JsonConvert.SerializeObject(BookingData.GenerateUserData());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            var response = await client.PostAsync(Endpoints.AuthenticationEndpoint(), postRequest);

            // Deserialize Content
            var responseData = JsonConvert.DeserializeObject<TokenModel>(response.Content.ReadAsStringAsync().Result);

            // Return token
            return responseData.token;
        }

    }
}
