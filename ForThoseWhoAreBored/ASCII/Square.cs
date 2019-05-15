using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII
{
    class Square
    {
        public Square(int x, int y)
        {
            X = x;
            Y = y;
            SquareSize_X = 5;
            SquareSize_Y = 3;
            IsChecked = false;
            IsHighlightByCursor = false;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsChecked { get; set; }

        public bool IsHighlightByCursor { get; set; }


        public int SquareSize_X { get; private set; }
        public int SquareSize_Y { get; private set; }
        
    }
}
