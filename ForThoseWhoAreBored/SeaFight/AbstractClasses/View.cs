using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class View
    {
        private int curragePositionX;
        private int curragePositionY;

        public int CurragePositionX { get { return curragePositionX; } private set { if (value < 0) { curragePositionX = 0; } } }
        public int CurragePositionY { get { return curragePositionY; } private set { if (value < 0) { curragePositionY = 0; } } }

        public View(int curragePositionX, int curragePositionY)
        {
            this.curragePositionX = curragePositionX;
            this.curragePositionY = curragePositionY;
 
           
        }

        public abstract void PrintView();

        public abstract void RemoveViewFromScreen();

    }
}
