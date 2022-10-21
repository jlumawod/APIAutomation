using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3
{
    public class GeneratePet
    {

        public static PetModel samplePet(int id)
        {
            return new PetModel
            {
                id = id,
                category = new Category()
                {
                    id = 0,
                    name = ""
                },
                name = "TestPet001",
                photoUrls = new List<string>
                {
                    "running_photo",
                    "sleeping_photo"
                },
                status = "available",
                tags = new List<Tag>()
                {
                    new Tag()
                    {
                        id = 0,
                        name = "Kid Friendly"
                    },
                    new Tag()
                    {
                        id = 1,
                        name = "Sleepy"
                    }
                }

            };
        }
    }
}
