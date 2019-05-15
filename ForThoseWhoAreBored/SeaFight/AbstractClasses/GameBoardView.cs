using SeaFight.Printers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class GameBoardView : View
    {
        public GameBoardView(int curragePositionX, int curragePositionY) : base(curragePositionX, curragePositionY)
        {
        }

        public abstract Square GetChoosenItem();

    }
}
