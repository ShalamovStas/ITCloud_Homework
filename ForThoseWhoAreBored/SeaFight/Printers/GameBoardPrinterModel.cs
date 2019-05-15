using SeaFight.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Printers
{
    class GameBoardPrinterModel : GameBoardPrinter
    {
        private readonly Square[,] squares;
        private int carriageStartPositionFrame_X;
        private int carriageStartPositionFrame_Y;

        private int carriageStartPositionBoardElements_X;
        private int carriageStartPositionBoardElements_Y;

        private int squareSize_X;
        private int squareSize_Y;

        private int countBoardElements_Axe_X;
        private int countBoardElements_Axe_Y;

        

        public GameBoardPrinterModel(int cursorStartPosition_X, int cursorStartPosition_Y, Square[,]squares) : base(cursorStartPosition_X, cursorStartPosition_Y)
        {
            this.squares = squares;
            carriageStartPositionFrame_X = CursorStartPosition_X;
            carriageStartPositionFrame_Y = CursorStartPosition_Y;

            carriageStartPositionBoardElements_X = carriageStartPositionFrame_X + 1;
            carriageStartPositionBoardElements_Y = carriageStartPositionFrame_Y + 1;

            squareSize_X = squares[0,0].SquareSize_X;
            squareSize_Y = squares[0,0].SquareSize_Y;

            countBoardElements_Axe_X = squares.GetLength(0);
            countBoardElements_Axe_Y = squares.GetLength(1);



        }


        //CursorStartPosition_X = 4;
        //CursorStartPosition_Y = 17;
        //this.squares = squares;


        public override void PrintView()
        {
            carriageStartPositionFrame_X = CursorStartPosition_X;
            carriageStartPositionFrame_Y = CursorStartPosition_Y;

            DrawFrame();
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    DrawSquare(squares[j,i]);
                }
            }
           

            DrawAxes();
        }

        private void DrawFrame()
        {
            string title = "Game Board";
            int tempTitleWidth = countBoardElements_Axe_X * squareSize_X + 1;
            int countOfGaps = tempTitleWidth - title.Length;
            int elementsCountRight = 0;
            int elementsCountLeft = 0;
            if(countOfGaps % 2 == 0)
            {
                elementsCountRight = countOfGaps/2;
                elementsCountLeft = countOfGaps / 2;
            }
            else
            {
                elementsCountRight = countOfGaps / 2 + 1;
                elementsCountLeft = countOfGaps / 2;
            }
            Console.SetCursorPosition(carriageStartPositionFrame_X, carriageStartPositionFrame_Y);
            Console.WriteLine(
                "┌" 
                + new string('─', elementsCountLeft) 
                + title
                + new string('─', elementsCountRight) 
                + "┐");
            
            //Console.WriteLine("│░░░░░░░░░░░░░░░░░░░░░░Game Board░░░░░░░░░░░░░░░░░░░░░░░│");
           
            for (int i = 0; i < countBoardElements_Axe_X; i++)
            {
                
                for (int j = 0; j < squareSize_Y; j++)
                {
                    Console.SetCursorPosition(carriageStartPositionFrame_X, Console.CursorTop);
                    Console.WriteLine("│" + new string(' ', countBoardElements_Axe_X * squareSize_X + 1) + "│");
                }
              
            }
            Console.SetCursorPosition(carriageStartPositionFrame_X, Console.CursorTop);
            Console.WriteLine("│" + new string(' ', countBoardElements_Axe_X * squareSize_X + 1) + "│");
            Console.SetCursorPosition(carriageStartPositionFrame_X, Console.CursorTop);
            Console.WriteLine("└" + new string('─', countBoardElements_Axe_X * squareSize_X + 1) + "┘");

        }

        private void DrawSquare(Square square)
        {
            Console.SetCursorPosition(square.X * squareSize_X + carriageStartPositionBoardElements_X, square.Y * squareSize_Y + carriageStartPositionBoardElements_Y);

            if (square.IsChecked)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            if (square.IsPartOfShip)
            {
                Console.ForegroundColor = square.Color;
            }

           

            if (square.IsHighlightByCursor)
            {
                Console.WriteLine("╔───╗");
                Console.SetCursorPosition(square.X * squareSize_X + carriageStartPositionBoardElements_X,
                    square.Y * squareSize_Y + carriageStartPositionBoardElements_Y + 1);
                Console.WriteLine("│ X │");
                Console.SetCursorPosition(square.X * squareSize_X + carriageStartPositionBoardElements_X,
                   square.Y * squareSize_Y + carriageStartPositionBoardElements_Y + 2);
                Console.WriteLine("╚───╝");
                square.IsHighlightByCursor = false;
            }
            else
            {
                Console.WriteLine("┌───┐");
                Console.SetCursorPosition(square.X * squareSize_X + carriageStartPositionBoardElements_X,
                    square.Y * squareSize_Y + carriageStartPositionBoardElements_Y + 1);
                Console.WriteLine("│   │");
                Console.SetCursorPosition(square.X * squareSize_X + carriageStartPositionBoardElements_X,
                   square.Y * squareSize_Y + carriageStartPositionBoardElements_Y + 2);
                Console.WriteLine("└───┘");
            }


            Console.ForegroundColor = ConsoleColor.White;
        }

        private void DrawAxes()
        {
            char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
            Console.SetCursorPosition(countBoardElements_Axe_Y * squareSize_X + carriageStartPositionBoardElements_X, 1 + carriageStartPositionBoardElements_Y);
            for (int i = 0; i < countBoardElements_Axe_Y; i++)
            {
                Console.WriteLine(i);
                Console.SetCursorPosition(countBoardElements_Axe_X * squareSize_X + carriageStartPositionBoardElements_X, Console.CursorTop + 2);
            }
            Console.SetCursorPosition(carriageStartPositionBoardElements_X, countBoardElements_Axe_Y * squareSize_Y + carriageStartPositionBoardElements_Y);


            for (int i = 0; i < countBoardElements_Axe_X; i++)
            {
                Console.Write("  {0}  ", alphabet[i]);
            }
            Console.WriteLine();
        }

        public override void BackspaceCursorFromPosition(int x, int y)
        {
            Console.SetCursorPosition(x * squareSize_X + carriageStartPositionBoardElements_X + 2,
                y * squareSize_Y + carriageStartPositionBoardElements_Y + 1);
            Console.Write(" ");
        }

        public override void PrintCursorToPosition(int x, int y)
        {
            Console.SetCursorPosition(x * squareSize_X + carriageStartPositionBoardElements_X + 2,
                            y * squareSize_Y + carriageStartPositionBoardElements_Y + 1);
            Console.Write("■");
        }

        public override void RemoveView()
        {


            //for (int i = 0; i < squares.GetLength(0); i++)
            //{
            //    for (int j = 0; j < squares.GetLength(1); j++)
            //    {
            //        RemoveSquare(squares[j,i]);
            //    }
            //}
            //RemoveAxes();
            RemoveFrame();

        }

        private void RemoveFrame()
        {
            Console.SetCursorPosition(carriageStartPositionFrame_X, carriageStartPositionFrame_Y);
            Console.WriteLine(new string(' ', countBoardElements_Axe_X * squareSize_X + 3));

            //Console.WriteLine("│░░░░░░░░░░░░░░░░░░░░░░Game Board░░░░░░░░░░░░░░░░░░░░░░░│");

            for (int i = 0; i < countBoardElements_Axe_X; i++)
            {

                for (int j = 0; j < squareSize_Y; j++)
                {
                    Console.SetCursorPosition(carriageStartPositionFrame_X, Console.CursorTop);
                    Console.WriteLine(new string(' ', countBoardElements_Axe_X * squareSize_X + 3));
                }

            }
            Console.SetCursorPosition(carriageStartPositionFrame_X, Console.CursorTop);
            Console.WriteLine(new string(' ', countBoardElements_Axe_X * squareSize_X + 3));
            Console.SetCursorPosition(carriageStartPositionFrame_X, Console.CursorTop);
            Console.WriteLine(new string(' ', countBoardElements_Axe_X * squareSize_X + 3));
        }

        private void RemoveSquare(Square square)
        {
            Console.SetCursorPosition(square.X * square.SquareSize_X + CursorStartPosition_X, square.Y * square.SquareSize_Y + CursorStartPosition_Y);

                Console.WriteLine("     ");
                Console.SetCursorPosition(square.X * square.SquareSize_X + CursorStartPosition_X, square.Y * square.SquareSize_Y + 1 + CursorStartPosition_Y);
                Console.WriteLine("     ");
                Console.SetCursorPosition(square.X * square.SquareSize_X + CursorStartPosition_X, square.Y * square.SquareSize_Y + 2 + CursorStartPosition_Y);
                Console.WriteLine("     ");

        }

        private void RemoveAxes()
        {
            Console.SetCursorPosition(10 * squares[0,0].SquareSize_X + CursorStartPosition_X, 1 + CursorStartPosition_Y);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(" ");
                Console.SetCursorPosition(10 * squares[0,0].SquareSize_X + CursorStartPosition_X, Console.CursorTop + 2);
            }
            Console.SetCursorPosition(CursorStartPosition_X, 10 * squares[0,0].SquareSize_Y + CursorStartPosition_Y);

            for (int i = 0; i < 10; i++)
            {
                Console.Write("     ");
            }
            Console.WriteLine();
        }





    }
}
