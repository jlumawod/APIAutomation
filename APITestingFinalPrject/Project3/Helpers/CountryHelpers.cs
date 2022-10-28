using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Helpers
{
    public class CountryHelpers
    {
        /// <summary>
        /// Returns a subtring from a country info
        /// </summary>
        /// <param name="str"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetSubString(string str, string info)
        {
            int index = str.IndexOf(" ");
            var isoCode = str.Substring(0, index);
            var countryName = str.Substring(index + 1, (str.Length-1) - index);
            return info.Equals("sIsoCode") ? isoCode : countryName;           
        }
    }
}
