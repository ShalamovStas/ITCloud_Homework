using SeaFight.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Tools
{
    class MessageBarPrinterModel : MessageBarPrinter
    {
        private int messageBarWidth;
        public string Title { get; set; }
        private string OldMessage;
        private ConsoleColor textColor;
        public MessageBarPrinterModel(int cursorStartPosition_X, int cursorStartPosition_Y, string title, int width, ConsoleColor textColor) : base(cursorStartPosition_X, cursorStartPosition_Y)
        {
            Title = title;
            OldMessage = "";
            messageBarWidth = width;
            this.textColor = textColor;
        }



        //public MainMessageBoxPrinter()
        //{
        //    CursorStartPosition_X = 0;
        //    CursorStartPosition_Y = 7;

        //}

        override public void PrintView()
        {

            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y);
            Console.WriteLine("┌" + new string('─', messageBarWidth - 2) + "┐");
            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y + 1);

            #region CalculateSizeFirstLine


            int leftGapsNumber = 0;
            int rightGapsNumber = 0;
            if ((messageBarWidth - 2 - Title.Length) % 2 == 0)
            {
                rightGapsNumber = (messageBarWidth - Title.Length - 2) / 2;
                leftGapsNumber = (messageBarWidth - Title.Length - 2) / 2;

            }
            else
            {
                rightGapsNumber = (messageBarWidth - Title.Length - 2) / 2 + 1;
                leftGapsNumber = (messageBarWidth - Title.Length - 2) / 2;
            }

            #endregion

            Console.WriteLine("│" + new string(' ', leftGapsNumber) + Title + new string(' ', rightGapsNumber) + "│");
            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y + 2);
            Console.WriteLine("├" + new string('─', messageBarWidth - 2) + "┤");
            PrintMessage("");

        }

        public override void PrintMessage(string message)
        {
            ClearOldMessage();
            OldMessage = message;
            string[] messages = message.Split('\n');


            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y + 3);
            for (int i = 0; i < messages.Length; i++)
            {
                Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
                Console.Write("│");
                Console.ForegroundColor = textColor;

                //if((messageBarWidth - messages[i].Length - 2) < 0)

                if (messageBarWidth - 2 < messages[i].Length)
                {

                    messages[i] = messages[i].Substring(0, messageBarWidth - 2);

                };
                int length = messageBarWidth - messages[i].Length - 2;
                Console.Write("{0}{1}", messages[i], new String(' ', length));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("│");
            }

            Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            Console.WriteLine("└" + new string('─', messageBarWidth - 2) + "┘");

        }

        private void ClearOldMessage()
        {

            string[] messages = OldMessage.Split('\n');
            Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y + 3);
            for (int i = 0; i < messages.Length + 2; i++)
            {
                Console.WriteLine(new string(' ', messageBarWidth));
                Console.SetCursorPosition(CursorStartPosition_X, Console.CursorTop);
            }

        }

        public override void RemoveView()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(CursorStartPosition_X, CursorStartPosition_Y + i);
                Console.WriteLine(new string(' ', messageBarWidth));
            }

            ClearOldMessage();
        }
    }
}
