using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII
{
    public class NewCar : Car
    {
        public NewCar(string color) : base(color)
        {

        }

        public override void Drive()
        {
            Console.WriteLine("Drive new car");
        }
    }
}
