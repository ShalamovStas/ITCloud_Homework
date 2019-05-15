
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII
{
    class Program
    {
        
        static void Main(string[] args)
        {
            LogicCreateShip logicCreateShip = new LogicCreateShip();
            Square[] parts1 = new[] { new Square(2, 10), new Square(2, 2), new Square(2, 3) };
            Ship ship;

            if(logicCreateShip.IsItPossibleToCreateShip(parts1, 3))
            {
                ship = logicCreateShip.GetShip(parts1);
                Console.WriteLine("Success!!");

            }
            else
            {
                Console.WriteLine(logicCreateShip.GetErrorMessage());
            }



            Console.WriteLine("Hello");
            Console.ReadKey();
        }

        

        

        


    }

  
}
