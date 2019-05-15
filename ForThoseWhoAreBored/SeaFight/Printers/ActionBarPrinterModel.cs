using SeaFight.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Tools
{
    class ActionBarPrinterModel : ActionBarPrinter
    {
        public ActionBarPrinterModel(int cursorStartPosition_X, int cursorStartPosition_Y, string[] menuItems, string title)
            : base(cursorStartPosition_X, cursorStartPosition_Y, menuItems, title)
        {

        }

        public override void BackspaceCursorFromPosition(int position)
        {
            Console.SetCursorPosition(CursorStartPosition_X + 2, CursorStartPosition_Y + 2 + position);
            Console.Write(" ");
            //Console.SetCursorPosition((CursorStartPosition_X +2) *2, CursorStartPosition_Y + 2 + position);
        }

        public override void PrintCursorToPosition(int position)
        {
            Console.SetCursorPosition(CursorStartPosition_X + 2, CursorStartPosition_Y + 2 + position);
            Console.Write(">");
            //Console.SetCursorPosition((CursorStartPosition_X + 2) * 2, CursorStartPosition_Y + 2 + position);


        }

        override public void PrintView()
        {
            //<-----------------------50----------------------->
            // <----------------------48---------------------->

            //║░░░░░░░░░░░░░░░░░░░Action Bar░░░░░░░░░░░░░░░░░░░║
            int actionBarWidth = 48;
            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y);
            Console.WriteLine("╓────────────────────────────────────────────────╖");
            Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            int leftGapsNumber = (actionBarWidth - Title.Length) / 2;
            int rightGapsNumber = 0;
            if (leftGapsNumber * 2 + Title.Length < actionBarWidth)
            {
                rightGapsNumber = leftGapsNumber + 1;

            }
            else if (leftGapsNumber * 2 + Title.Length > actionBarWidth)
            {
                rightGapsNumber = leftGapsNumber - 1;
            }
            else
            {
                rightGapsNumber = leftGapsNumber;
            }


            Console.WriteLine("║" + new string('░', leftGapsNumber) + Title + new string('░', rightGapsNumber) + "║");
            Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            //Console.WriteLine("║░░░░░░░░░░░░░░░░░░░Action Bar░░░░░░░░░░░░░░░░░░░║");
            var length = MenuItems.Length;

            for (int i = 0; i < length; i++)
            {
                Console.WriteLine("║   {0}{1}║", MenuItems[i], new String(' ', 45 - MenuItems[i].Length));
                Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);

            }

            Console.WriteLine("╙────────────────────────────────────────────────╜");
        }

        public override void RemoveView()
        {
            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y);
            Console.WriteLine("                                                  ");
            Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            Console.WriteLine("                                                  ");
            Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            Console.WriteLine("                                                  ");
            for (int i = 0; i < MenuItems.Length; i++)
            {
                Console.WriteLine("                                                  ");
                Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            }

        }
    }
}
