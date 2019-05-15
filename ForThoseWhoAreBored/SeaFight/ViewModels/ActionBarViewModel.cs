using SeaFight.AbstractClasses;
using SeaFight.Common;
using SeaFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight
{


    class ActionBarViewModel : ActionBarView
    {

        private int pointerCurrentPosition;

        public ActionBarViewModel(int cursorStartPosition_X, int cursorStartPosition_Y, string[] actionBarItems, string title) : base(cursorStartPosition_X, cursorStartPosition_Y, actionBarItems, title)
        {
            pointerCurrentPosition = 0;
            
        

        }

        public void ShowPointerCurrentPosition()
        {
            Printer.PrintCursorToPosition(pointerCurrentPosition);

        }

        override public void MovePointerDown()
        {
            Printer.BackspaceCursorFromPosition(pointerCurrentPosition);
           
            pointerCurrentPosition++;
            var length = ActionBarItems.Length;
            if (pointerCurrentPosition > length - 1)
            {
                pointerCurrentPosition = 0;
            }

            Printer.PrintCursorToPosition(pointerCurrentPosition);
        }

        override public void MovePointerLeft()
        {
            MovePointerUp();
        }

        override public void MovePointerRight()
        {
            MovePointerDown();
        }

        override public void MovePointerUp()
        {
            Printer.BackspaceCursorFromPosition(pointerCurrentPosition);
            pointerCurrentPosition--;

            var length = ActionBarItems.Length;
            if (pointerCurrentPosition < 0)
            {
                pointerCurrentPosition = length - 1;
            }
            Printer.PrintCursorToPosition(pointerCurrentPosition);
        }

        public string ChooseItem()
        {
            return ActionBarItems[pointerCurrentPosition];
        }

        public override void PrintView()
        {
            Printer.PrintView();
        }

        public override void RemoveViewFromScreen()
        {
            Printer.RemoveView();
        }
    }
}
