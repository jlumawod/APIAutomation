using CountryServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Helpers;

[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace Project3.Test
{
    [TestClass]
    public class CountryTests : BaseTest
    {
        /// <summary>
        /// Get list of countries with code and names
        /// </summary>
        /// <returns>List<tCountryCodeAndName></returns>
        private List<tCountryCodeAndName> GetListOfCountries()
        {
            return countryTest.ListOfCountryNamesByCode();
        }

        /// <summary>
        /// Get a random country from a list of countries
        /// </summary>
        /// <param name="countryList"></param>
        /// <returns>string</returns>
        private string GetRandomRecord(List<tCountryCodeAndName> countryList)
        {
            Random random = new Random();
            var country = countryList[random.Next(0, countryList.Count - 1)];
            return $"{country.sISOCode} {country.sName}";
        }

        
        [TestMethod]
        public void GetCountry()
        {
            //Get list of countries from private method GetListOfCountries()
            var countryList = GetListOfCountries();

            //Get random record using list of countries
            var randomRecord = GetRandomRecord(countryList);

            //Get country info using the random record
            var fullCountryInfo = countryTest.FullCountryInfo(CountryHelpers.GetSubString(randomRecord, "sIsoCode"));

            //Assert that all info are correct and valid
            Assert.AreEqual(fullCountryInfo.sISOCode, CountryHelpers.GetSubString(randomRecord, "sIsoCode"), "sIsoCode Do Not Match");
            Assert.AreEqual(fullCountryInfo.sName, CountryHelpers.GetSubString(randomRecord, "sName"), "sName Do Not Match");
        }

        [TestMethod]
        public void SelectRandomRecordsAndVerify()
        {
            //Get list of countries
            var countryList = GetListOfCountries();
            
            //get list of 5 countries randomly
            var selectCountryList = Enumerable.Range(0, 4).Select(_ => GetRandomRecord(countryList)).ToList();

            //Loop thru each randomly selected countries
            foreach(string selectCountry in selectCountryList)
            {
                //Get country code of selected country country
                var countryCode = countryTest.CountryISOCode(CountryHelpers.GetSubString(selectCountry,"sName"));

                //Assert if country codes are equal
                Assert.AreEqual(countryCode, CountryHelpers.GetSubString(selectCountry, "sIsoCode"));
            }
        }
    }
}
