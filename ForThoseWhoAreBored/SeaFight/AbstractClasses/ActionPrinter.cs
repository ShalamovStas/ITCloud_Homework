using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract public class ActionBarPrinter : Printer
    {
        public string[] MenuItems { get; set; }
        public string Title { get; set; }

        public ActionBarPrinter(int cursorStartPosition_X, int cursorStartPosition_Y, string[] menuItems, string title) : base(cursorStartPosition_X, cursorStartPosition_Y)
        {
            MenuItems = menuItems;
            Title = title;
        }

        
        public abstract void BackspaceCursorFromPosition(int position);
        public abstract void PrintCursorToPosition(int position);
    }
}
