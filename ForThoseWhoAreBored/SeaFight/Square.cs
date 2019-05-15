using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight
{
    class Square
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int SquareSize_X { get; private set; }
        public int SquareSize_Y { get; private set; }

        public bool IsChecked { get; set; }
        public bool IsHighlightByCursor { get; set; }
        public bool IsPartOfShip { get; set; }

        public ConsoleColor Color { get; set; }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
            SquareSize_X = 5;
            SquareSize_Y = 3;
            IsChecked = false;
            IsHighlightByCursor = false;
            IsPartOfShip = false;
            Color = ConsoleColor.White;
        }
       
        
    }
}
