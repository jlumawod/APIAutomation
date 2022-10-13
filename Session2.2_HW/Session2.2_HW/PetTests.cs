using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Session2._2_HW
{
    [TestClass]
    public class PetTests
    {

        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string PetEndPoint = "pet";

        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<Pet> cleanUpList = new List<Pet>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{PetEndPoint}/{data.id}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task AddNewPet()
        {
            #region Create Pet Object
            //Create User
            Pet pet = new Pet()
            {
                id = 2025,
                category = new Category()
                {
                    id = 0,
                    name = "new"
                },
                name = "Charlie",
                photoUrls = new string[]
                {
                    "Hover",
                    "Stare"
                },
                tags = new List<Tag>
                {
                    new Tag()
                    {
                        id = 0,
                        name = "Champ"
                    }
                },
                status = "available"
            };

            // Send Post Request            
            var postRestRequest = new RestRequest(GetURI(PetEndPoint)).AddJsonBody(pet);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{PetEndPoint}/{pet.id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<Pet>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(pet.name, restResponse.Data.name, "Pet Name did not match.");           
            Assert.AreEqual(pet.status, restResponse.Data.status, "Pet Status did not match.");
            Assert.AreEqual(pet.category.name, restResponse.Data.category.name, "Pet Category did not match.");
            Assert.IsTrue(pet.photoUrls.SequenceEqual(restResponse.Data.photoUrls), "Pet Photos did not match.");

            for (int i = 0; i < pet.tags.Count; i++)
            {
                Assert.AreEqual(pet.tags[i].name, restResponse.Data.tags[i].name, "Pet Tags did not match.");
            }


            #endregion

            #region CleanUp
            cleanUpList.Add(pet);
            #endregion
        }
    }
}
