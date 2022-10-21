using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Session3
{

    [TestClass]
    public class PetTests : ApiBaseTest
    {

        private static List<PetModel> petCleanUpList = new List<PetModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            this.PetDetails = await PetHelper.AddNewPet(RestClient, PetId);
        }

        [TestMethod]
        public async Task GetPet()
        {

            //Arrange
            var demoGetRequest = new RestRequest(Endpoints.GetPetById(PetDetails.id));
            petCleanUpList.Add(PetDetails);

            //Act
            var demoResponse = await RestClient.ExecuteGetAsync<PetModel>(demoGetRequest);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, demoResponse.StatusCode, "Failed due to wrong status code.");
            Assert.AreEqual(PetDetails.name, demoResponse.Data.name);
            Assert.AreEqual(PetDetails.status, demoResponse.Data.status);
            Assert.IsTrue(PetDetails.photoUrls.SequenceEqual(demoResponse.Data.photoUrls));
            Assert.IsTrue(PetDetails.tags.SequenceEqual(demoResponse.Data.tags, new TagComparer()),"Tags do not match");

        }
    }
}
