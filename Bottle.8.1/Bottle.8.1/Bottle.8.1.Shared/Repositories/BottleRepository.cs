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
                new BottleInfo{Path = "ms-appx:///Images/bottle/1.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/3.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/4.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/6.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/7.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/10.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/11.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/12.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/13.png"},
                new BottleInfo{Path = "ms-appx:///Images/bottle/16.png"}
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
