using SeaFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class ActionBarView : View

       
    {

        public ActionBarPrinter Printer { get; set; }
        public string[] ActionBarItems { get; set; }





        public ActionBarView(int curragePosition_X, int curragePosition_Y, string[] actionBarItems, string title) : base(curragePosition_X, curragePosition_Y)
        {
            ActionBarItems = actionBarItems;
            Printer = new ActionBarPrinterModel(CurragePositionX, CurragePositionY, actionBarItems, title);
        }

        
        

        public abstract void MovePointerRight();
        public abstract void MovePointerLeft();
        public abstract void MovePointerUp();
        public abstract void MovePointerDown();
    }
}
