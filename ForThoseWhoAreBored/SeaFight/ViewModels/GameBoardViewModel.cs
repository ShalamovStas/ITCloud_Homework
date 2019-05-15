using SeaFight.AbstractClasses;
using SeaFight.Printers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight
{
    class GameBoardViewModel : GameBoardView
    {

        private GameBoardPrinter BoardPrinter { get; set; }
        private int BoardSize { get; set; }


        private int cursorCurrentPositionX;
        private int cursorCurrentPositionY;
        private readonly Square[,] squares;



        public GameBoardViewModel(int curragePosition_X, int curragePosition_Y): base (curragePosition_X, curragePosition_Y)
        {
            BoardSize = 10;
            squares = new Square[BoardSize, BoardSize];
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    squares[j, i] = new Square(j, i);
                }
            }


            BoardPrinter = new GameBoardPrinterModel(curragePosition_X, curragePosition_Y, squares);
            cursorCurrentPositionX = 0;
            cursorCurrentPositionY = 0;
        }



        public void MovePointerDown()
        {

            BoardPrinter.BackspaceCursorFromPosition(cursorCurrentPositionX, cursorCurrentPositionY);
            cursorCurrentPositionY++;
            if (cursorCurrentPositionY == BoardSize)
            {
                cursorCurrentPositionY = 0;
            }
            BoardPrinter.PrintCursorToPosition(cursorCurrentPositionX, cursorCurrentPositionY);

        }

        public void MovePointerLeft()
        {
            BoardPrinter.BackspaceCursorFromPosition(cursorCurrentPositionX, cursorCurrentPositionY);

            cursorCurrentPositionX--;
            if (cursorCurrentPositionX == -1)
            {
                cursorCurrentPositionX = BoardSize - 1;
            }
            BoardPrinter.PrintCursorToPosition(cursorCurrentPositionX, cursorCurrentPositionY);

        }

        public void MovePointerRight()
        {
            BoardPrinter.BackspaceCursorFromPosition(cursorCurrentPositionX, cursorCurrentPositionY);

            cursorCurrentPositionX++;
            if (cursorCurrentPositionX == BoardSize)
            {
                cursorCurrentPositionX = 0;
            }
            BoardPrinter.PrintCursorToPosition(cursorCurrentPositionX, cursorCurrentPositionY);

        }

        public void MovePointerUp()
        {
            BoardPrinter.BackspaceCursorFromPosition(cursorCurrentPositionX, cursorCurrentPositionY);
            cursorCurrentPositionY--;
            if (cursorCurrentPositionY == -1)
            {
                cursorCurrentPositionY = BoardSize - 1;
            }
            BoardPrinter.PrintCursorToPosition(cursorCurrentPositionX, cursorCurrentPositionY);

        }

        public override void PrintView()
        {
            BoardPrinter.CursorStartPosition_X = CurragePositionX;
            BoardPrinter.CursorStartPosition_Y = CurragePositionY;
            BoardPrinter.PrintView();
        }

        public override Square GetChoosenItem()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (squares[j, i].X == cursorCurrentPositionX && squares[j, i].Y == cursorCurrentPositionY)
                    {
                        return squares[j, i];
                    }

                }
            }


            return new Square(0, 0);
        }

        public override void RemoveViewFromScreen()
        {
            BoardPrinter.RemoveView();
        }
    }
}
