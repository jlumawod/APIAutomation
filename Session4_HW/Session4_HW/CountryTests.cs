using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountryServices;

namespace Session4_HW
{
    [TestClass]
    public class CountryTests
    {
        private static CountryInfoServiceSoapTypeClient countryInfoServiceSoapType = null;

        [TestInitialize]
        public void TestInit()
        {
            countryInfoServiceSoapType = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public void ValidateCountryListAscending()
        {
            // Get list of Countries
            var countryNamesList =  countryInfoServiceSoapType.ListOfCountryNamesByCode();

            // Order list by code
            var ascendingOrder = countryNamesList.OrderBy(a => a.sISOCode);

            // Assert if country list is in ascending order
            Assert.IsTrue(countryNamesList.SequenceEqual(ascendingOrder), "Not Ascending");

        }


        [TestMethod]
        public void ValidateInvalidCountryCode()            
        {
            // Use Invalid Data for Country Code
            string countryCode = "asdad";

            // Search for Country using invalid data
            var countryName = countryInfoServiceSoapType.CountryName(countryCode);

            // Assert if country returned is not found
            Assert.AreEqual("Country not found in the database", countryName, "Country is valid");

        }


        [TestMethod]
        public void GetLastEntry()
        {
            // Get list of Countries
            var countryNamesList = countryInfoServiceSoapType.ListOfCountryNamesByCode();

            // Get last country from the list
            var country = countryNamesList.Last();
            
            // Get country name from using code of the last country
            var countryName = countryInfoServiceSoapType.CountryName(country.sISOCode);

            // Assert that the country names are equal
            Assert.AreEqual(country.sName, countryName, "Country don't match"); 

        }


    }
}
