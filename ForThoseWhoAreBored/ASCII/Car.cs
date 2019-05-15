using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII
{
    public abstract class Car
    {
        public string Color { get; set; }
        public Car(string color)
        {
            Color = color;
        }
        
        public virtual void Drive() { }
    }
}
