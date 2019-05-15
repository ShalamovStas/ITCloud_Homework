using SeaFight.Session;
using SeaFight.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Logic
{
    class BattleshipLogic
    {
        public string CurrentMessage { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool ArrangingShipIsFinished { get; private set; }
        public string StatusTable
        {
            get { return arrangeShipsLogic.StatusTable; }
           
        }

        private Ship[] userShips;
        private Ship[] computerShips;

        private int numberOfShips;
        private int currentShipIndex;

        private Gameprocess gameprocess;

        private ArrangeShipsLogic arrangeShipsLogic;


        public BattleshipLogic()
        {
            numberOfShips = 10;
            userShips = new Ship[numberOfShips];
            computerShips = new Ship[numberOfShips];
            gameprocess = Gameprocess.ArrangeShips;
            ArrangingShipIsFinished = false;
            arrangeShipsLogic = new ArrangeShipsLogic();
            CurrentMessage = arrangeShipsLogic.CurrentMessage;
        }


        public int ArrangeShip(Square[] squares)
        {
            int result = 0;
            if (gameprocess != Gameprocess.ArrangeShips)
            {
                return -1;
            }

            result = arrangeShipsLogic.ArrangeShip(squares);
            switch (result)
            {
                case -1:
                    ErrorMessage = arrangeShipsLogic.ErrorMessage;
                    break;
                case 1:
                    userShips[currentShipIndex] = arrangeShipsLogic.Ship;
                    currentShipIndex++;
                    CurrentMessage = arrangeShipsLogic.CurrentMessage;
                    if (currentShipIndex == userShips.Length)
                    {
                        ArrangingShipIsFinished = true;
                        gameprocess = Gameprocess.Fighting;
                        CurrentMessage = "Arranging ships is finished\nYou can start battle!";
                    }
                    break;
            }

            return result;
        }




        private enum Gameprocess
        {
            ArrangeShips,
            Fighting
        }


    }
}


