using CountryServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Helpers;

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
            var countryList = GetListOfCountries();
            var randomRecord = GetRandomRecord(countryList);

            var fullCountryInfo = countryTest.FullCountryInfo(CountryHelpers.GetSubString(randomRecord, "sIsoCode"));

            Assert.AreEqual(fullCountryInfo.sISOCode, CountryHelpers.GetSubString(randomRecord, "sIsoCode"), "sIsoCode Do Not Match");
            Assert.AreEqual(fullCountryInfo.sName, CountryHelpers.GetSubString(randomRecord, "sName"), "sName Do Not Match");
        }

        [TestMethod]
        public void SelectRandomRecordsAndVerify()
        {
            var countryList = GetListOfCountries();
            var selectCountryList = Enumerable.Range(0, 4).Select(_ => GetRandomRecord(countryList)).ToList();

            foreach(string selectCountry in selectCountryList)
            {
                var countryCode = countryTest.CountryISOCode(CountryHelpers.GetSubString(selectCountry,"sName"));

                Assert.AreEqual(countryCode, CountryHelpers.GetSubString(selectCountry, "sIsoCode"));
            }
        }
    }
}
