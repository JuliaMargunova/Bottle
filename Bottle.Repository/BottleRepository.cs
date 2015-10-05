using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bottle.Entities;

namespace Bottle.Repository
{
    public class BottleRepository
    {
        List<BottleInfo> bottles;
        public BottleRepository()
        {
            bottles = new List<BottleInfo> {
                new BottleInfo{Path = "/Images/bottle/1.png"},
                new BottleInfo{Path = "/Images/bottle/2.png"},
                new BottleInfo{Path = "/Images/bottle/3.png"},
                new BottleInfo{Path = "/Images/bottle/4.png"},
                new BottleInfo{Path = "/Images/bottle/5.png"},
                new BottleInfo{Path = "/Images/bottle/6.png"},
                new BottleInfo{Path = "/Images/bottle/7.png"},
                new BottleInfo{Path = "/Images/bottle/9.png"},
                new BottleInfo{Path = "/Images/bottle/10.png"},
                new BottleInfo{Path = "/Images/bottle/11.png"}
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
