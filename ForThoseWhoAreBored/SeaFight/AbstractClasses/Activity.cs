using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class Activity
    {
        public IManager Manager { get; set; }
        public Activity(IManager manager)
        {
            Manager = manager;
        }
        abstract public void Run();
        abstract public void ShowActivity();
        abstract public void RemoveActivityFromScreen();
       
    }
}
