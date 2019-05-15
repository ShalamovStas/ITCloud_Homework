using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class GameBoardPrinter : Printer
    {
        public GameBoardPrinter(int cursorStartPosition_X, int cursorStartPosition_Y) : base(cursorStartPosition_X, cursorStartPosition_Y)
        {
        }

       
        public abstract void BackspaceCursorFromPosition(int x, int y);
        public abstract void PrintCursorToPosition(int x, int y);
        

    }
}
