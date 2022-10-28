using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Test
{
    public class BaseTest
    {
        public readonly CountryServiceReference.CountryInfoServiceSoapTypeClient countryTest =
             new CountryServiceReference.CountryInfoServiceSoapTypeClient(CountryServiceReference.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

    }
}
