using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3
{
    public class Endpoints
    {
        public const string baseURL = "https://petstore.swagger.io/v2";

        public static string GetPetById(int id) => $"{baseURL}/pet/{id}";

        public static string PostPet() => $"{baseURL}/pet";

    }
}
