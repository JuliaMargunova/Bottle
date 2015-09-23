using Bottle._8._1.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;

namespace Bottle._8._1.Repositories
{
    public class BackgroundRepository
    {
        //enum Colors { red }
        List<Background> backgrounds;
        public BackgroundRepository()
        {
            backgrounds = new List<Background> {
                new Background{Path = "ms-appx:///Images/background/kover.jpg", ColorLine = Colors.Red },
                new Background{Path = "ms-appx:///Images/background/serzewp.jpg", ColorLine = Colors.Yellow},
                new Background{Path = "ms-appx:///Images/background/derevowp.jpg", ColorLine = Colors.Orange},
                new Background{Path = "ms-appx:///Images/background/travawp.jpg", ColorLine = Colors.Red},
                new Background{Path = "ms-appx:///Images/background/kirpichwp.jpg", ColorLine = Colors.Orange},
                new Background{Path = "ms-appx:///Images/background/big1.jpg", ColorLine = Color.FromArgb(255,255,151,0)},
                new Background{Path = "ms-appx:///Images/background/big2.jpg", ColorLine = Color.FromArgb(255,0,174,255)},
                new Background{Path = "ms-appx:///Images/background/big4.jpg", ColorLine = Color.FromArgb(255,239,142,138)}
            };
        }

        public List<Background> GetBottles()
        {
            return backgrounds;
        }

        public Background GetBackground(int numberBackground)
        {
            return backgrounds[numberBackground - 1];
        }

        public int GetCountBackground()
        {
            return backgrounds.Count;
        }
    }
}
