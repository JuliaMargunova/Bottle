using Bottle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Bottle.Repository
{
    public class BackgroundRepository
    {
        //enum Colors { red }
        List<Background> backgrounds;
        public BackgroundRepository()
        {
            backgrounds = new List<Background> {
                new Background{Path = "/Images/background/big2.jpg", ColorLine = Color.FromArgb(255,0,174,255)},
                new Background{Path = "/Images/background/f1.jpg", ColorLine = Colors.Red },
                new Background{Path = "/Images/background/f2.jpg",ColorLine = Color.FromArgb(255,0,174,255) },
                new Background{Path = "/Images/background/f4.jpg",ColorLine = Colors.Yellow },
                new Background{Path = "/Images/background/f3.jpg",ColorLine = Colors.Red },
                new Background{Path = "/Images/background/kover.jpg", ColorLine = Colors.Red },
                new Background{Path = "/Images/background/serzewp.jpg", ColorLine = Colors.Yellow},
                new Background{Path = "/Images/background/derevowp.jpg", ColorLine = Colors.Orange},
                new Background{Path = "/Images/background/travawp.jpg", ColorLine = Colors.Red},
                new Background{Path = "/Images/background/kirpichwp.jpg", ColorLine = Colors.Orange},
                new Background{Path = "/Images/background/big1.jpg", ColorLine = Color.FromArgb(255,255,151,0)},
                new Background{Path = "/Images/background/big4.jpg", ColorLine = Color.FromArgb(255,239,142,138)}
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
            return backgrounds.Count();
        }
    }
}
