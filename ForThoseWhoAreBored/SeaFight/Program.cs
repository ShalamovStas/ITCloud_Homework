using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight
{
    class Program
    {
        static int currentCrossPositionX = 0;
        static int currentCrossPositionY = 0;
        static List<Square> squares = new List<Square>();
        static void Main(string[] args)
        {

            Console.CursorVisible = false;
           
            Engene engene = new Engene();
            engene.RunGame();
        }



        
    }
}
