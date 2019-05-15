using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class MessageBarPrinter : Printer
    {
        public MessageBarPrinter(int cursorStartPosition_X, int cursorStartPosition_Y) : base(cursorStartPosition_X, cursorStartPosition_Y)
        {
        }

        
        abstract public void PrintMessage(string message);
    }
}
