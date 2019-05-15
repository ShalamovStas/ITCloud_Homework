using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    public abstract class Printer
    {
        

        public int CursorStartPosition_X { get;  set; }
        public int CursorStartPosition_Y { get;  set; }

        public Printer(int cursorStartPosition_X, int cursorStartPosition_Y)
        {
           
            CursorStartPosition_X = cursorStartPosition_X;
            CursorStartPosition_Y = cursorStartPosition_Y;

        }

        public abstract void PrintView();
        public abstract void RemoveView();

    }
}
