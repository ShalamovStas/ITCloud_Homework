using SeaFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class MessageView : View
    {
        private MessageBarPrinter Printer { get; set; }

        public string Title { get; set; }


        public MessageView(int curragePosition_X, int curragePosition_Y, string title, int width, ConsoleColor textColor) : base (curragePosition_X, curragePosition_Y)
        {
            Title = title;
           
            Printer = new MessageBarPrinterModel(CurragePositionX, CurragePositionY, Title, width, textColor);
        }


        public virtual void ShowMessage(string message)
        {
            Printer.PrintMessage(message);
        }


        public override void PrintView()
        {
            Printer.CursorStartPosition_X = CurragePositionX;
            Printer.CursorStartPosition_Y = CurragePositionY;
            Printer.PrintView();
        }

        public override void RemoveViewFromScreen()
        {
            Printer.RemoveView();
        }
    }
}
