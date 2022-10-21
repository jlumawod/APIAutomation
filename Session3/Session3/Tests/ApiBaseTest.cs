using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3
{
    public class ApiBaseTest
    {
        public RestClient RestClient { get; set; }

        public PetModel PetDetails { get; set; }

        public int PetId = 2022;

        [TestInitialize]
        public void Initilize()
        {
            RestClient = new RestClient();
        }

        [TestCleanup]
        public void CleanUp()
        {

        }
    }
}
