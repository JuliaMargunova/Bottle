using Bottle.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Bottle.Repository
{
    public  class BackgroundRepository
    {
        //enum Colors { red }
        List<Background> backgrounds;
        public BackgroundRepository()
        {
            backgrounds = new List<Background> {
                new Background{Path = "/Images/background/kover.jpg", ColorLine = Colors.Red },
                new Background{Path = "/Images/background/serzewp.jpg", ColorLine = Colors.Yellow},
                new Background{Path = "/Images/background/derevowp.jpg", ColorLine = Colors.Orange},
                new Background{Path = "/Images/background/travawp.jpg", ColorLine = Colors.}, 
                new Background{Path = "/Images/background/kirpichwp.jpg"},
                new Background{Path = "/Images/background/oboi.jpg"}
            };
        }

        public List<Background> GetBottles()
        {
            return backgrounds;
        }

        public Background GetBottle(int numberBackground)
        {
            return backgrounds[numberBackground - 1];
        }

        public int GetCountBottle()
        {
            return backgrounds.Count();
        }
    }
}
