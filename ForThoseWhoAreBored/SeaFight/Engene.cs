using SeaFight.AbstractClasses;
using SeaFight.Activities;
using SeaFight.ViewModels;
using SeaFight.Printers;
using SeaFight.Session;
using SeaFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaFight.Managers;

namespace SeaFight
{

    class Engene
    {

        public void RunGame()
        {
        
            var mainManager = new MainManagerActivity();
            mainManager.Run();

        }

    }
}
