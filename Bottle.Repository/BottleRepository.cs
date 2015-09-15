using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bottle.Entities;

namespace Bottle.Repository
{
    public class BottleRepository
    {
        List<BottleInfo> bottles;
        public BottleRepository()
        {
            bottles = new List<BottleInfo> {
                new BottleInfo{Path = "/Images/bottle/martin.png" },
                new BottleInfo{Path = "/Images/bottle/bottle1.png"},
                new BottleInfo{Path = "/Images/bottle/vallo.png"},
                new BottleInfo{Path = "/Images/bottle/vallos.png"},
                new BottleInfo{Path = "/Images/bottle/finlandia.png" },
                new BottleInfo{Path = "/Images/bottle/medel.png" },
                new BottleInfo{Path = "/Images/bottle/zvezda.png" },
                new BottleInfo{Path = "/Images/bottle/bottle.png"}                
            };
        }

        public List<BottleInfo> GetBottles()
        {
            return bottles;
        }

        public BottleInfo GetBottle(int numberBottle)
        {
            return bottles[numberBottle - 1];
        }

        public int GetCountBottle()
        {
            return bottles.Count();
        }
    }
}
