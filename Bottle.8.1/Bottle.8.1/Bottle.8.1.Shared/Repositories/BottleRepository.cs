using Bottle._8._1.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bottle._8._1.Repositories
{
    public class BottleRepository
    {
        List<BottleInfo> bottles;
        public BottleRepository()
        {
            bottles = new List<BottleInfo> {
                new BottleInfo{Path = "ms-appx:///Images/bottle/martin.png" },
                new BottleInfo{Path = "ms-appx:///Images/bottle/bottle1.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/vallo.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/vallos.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/finlandia.png" },
                new BottleInfo{Path = "ms-appx:///Images/bottle/medel.png" },
                new BottleInfo{Path = "ms-appx:///Images/bottle/zvezda.png" },
                new BottleInfo{Path = "ms-appx:///Images/bottle/bottle.png"}
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
            return bottles.Count;
        }
    }
}
