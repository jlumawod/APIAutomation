using Project2.DataModels;
using RestSharp;
using Project2.Resources;
using Project2.Tests.TestData;
using System.Net;

namespace Project2.Helpers
{
    public class BookingHelper
    {

        public static async Task<RestResponse<BookingModel>> PostBooking()
        {
            RestClient client = new RestClient();
            client.AddDefaultHeader("Content-Type", "application/json");
            client.AddDefaultHeader("Accept", "application/json");

            var postRequest = new RestRequest(Endpoints.PostBooking()).AddJsonBody(BookingData.GenerateBookingData());
            
            return await client.ExecutePostAsync<BookingModel>(postRequest);           
        }

        public static async Task<RestResponse<BookingDetails>> GetBookingById(int id)
        {
            RestClient client = new RestClient();
            client.AddDefaultHeader("Accept", "application/json");
            var getRequest = new RestRequest(Endpoints.GetBookingById(id));

            return await client.ExecuteGetAsync<BookingDetails>(getRequest);
        }

        public static async Task<RestResponse<BookingDetails>> UpdateBookingById(BookingDetails bookingDetails, int bookingId)
        {
            RestClient client = new RestClient();
            client.AddDefaultHeader("Accept", "application/json");

            await AuthenticateUser(client);

            var putRequest = new RestRequest(Endpoints.UpdateBookingById(bookingId)).AddJsonBody(bookingDetails);

            return await client.ExecutePutAsync<BookingDetails>(putRequest);
        }

        public static async Task<RestResponse> DeleteBookingById(int id)
        {
            RestClient client = new RestClient();
            client.AddDefaultHeader("Accept", "application/json");

            await AuthenticateUser(client);

            var deleteRequest = new RestRequest(Endpoints.DeleteBooking(id));

            return await client.DeleteAsync(deleteRequest);
        }

        private static async Task AuthenticateUser(RestClient client)
        {
            // Get token
            var token = await BookingHelper.GetToken(client);

            // Set Request Header            
            client.AddDefaultHeader("Cookie", $"token={token}");
            

        }

        private static async Task<string> GetToken(RestClient restClient)
        {   
            var postRequest = new RestRequest(Endpoints.AuthenticationEndpoint()).AddJsonBody(BookingData.GenerateUserData());

            var getToken = await restClient.ExecutePostAsync<TokenModel>(postRequest);


            return getToken.Data.token;
        }
    }
}
