using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Session3
{
    public class PetHelper
    {
        public static async Task<PetModel> AddNewPet(RestClient client, int id)
        {
            var newPetData = GeneratePet.samplePet(id);
            var postRequest = new RestRequest(Endpoints.PostPet());

            //Send Post Request to add new pet
            postRequest.AddJsonBody(newPetData);
            var postResponse = await client.ExecutePostAsync<PetModel>(postRequest);

            var createdUserData = newPetData;
            return createdUserData;
        }
        
    }

    public class TagComparer : IEqualityComparer<Tag>
    {
        public bool Equals(Tag x, Tag y)
        {
            if (x == null || y == null) return false;

            bool equals = x.id == y.id && x.name== y.name ;
            return equals;
        }

        public int GetHashCode(Tag obj)
        {
            if (obj == null) return int.MinValue;

            int hash = 19;
            hash = hash + obj.id.GetHashCode();
            hash = hash + obj.name.GetHashCode();
            
            return hash;
        }
    }
}
