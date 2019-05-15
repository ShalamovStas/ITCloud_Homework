using SeaFight.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Logic
{
    class ArrangeShipsLogic
    {
        private LogicShipCreator shipCreator;
        public string ErrorMessage { get; private set; }
        public string CurrentMessage { get; private set; }


        private ArrangingShipsRrocess arrangingShipsRrocess;

        public Ship Ship { get; private set; }

        private int currentCountBattleships;
        private int currentCountCruisers;
        private int currentCountDestroyers;
        private int currentCountSubmarines;

        private readonly int maxCountBattleships;
        private readonly int maxCountCruisers;
        private readonly int maxCountDestroyers;
        private readonly int maxCountSubmarines;

        private int countElementsForBattleships;
        private int countElementsForCruisers;
        private int countElementsForDestroyers;
        private int countElementsForSubmarines;


        public string StatusTable
        {
            get
            {
                return "Battleships  "  + currentCountBattleships    + " from " + maxCountBattleships + "\n"
                  + "Cruisers     "     + currentCountCruisers       + " from " + maxCountCruisers + "\n"
                  + "Destroyers   "     + currentCountDestroyers     + " from " + maxCountDestroyers + "\n"
                  + "Submarines   "     + currentCountSubmarines     + " from " + maxCountSubmarines;
            }
        }

        /// 
        /// Battleships  0 from 1
        /// Cruisers     0 from 1
        /// Destroyers   0 from 1
        /// Submarines   0 from 1
        /// 


        public ArrangeShipsLogic()
        {
            shipCreator = new LogicShipCreator();
            arrangingShipsRrocess = ArrangingShipsRrocess.ArrangeBattleship;

            currentCountBattleships = 0;
            currentCountCruisers = 0;
            currentCountDestroyers = 0;
            currentCountSubmarines = 0;

            maxCountBattleships = 1;
            maxCountCruisers = 2;
            maxCountDestroyers = 3;
            maxCountSubmarines = 4;


            ///            	Size    Number per player 
            /// battleship  4       1
            /// cruiser     3       2
            /// destroyer   2       3
            /// submarine   1       4
            /// 
            /// 
            /// Number per player = maxCount......(maxCountBattleships)
            /// Size = countElementsFor.......(countElementsForBattleships)


            countElementsForBattleships = 4;
            countElementsForCruisers = 3;
            countElementsForDestroyers = 2;
            countElementsForSubmarines = 1;

            CurrentMessage = "You have to arrange Battleship: ■ ■ ■ ■\nNow you have " + currentCountBattleships + " Battleships from " + maxCountBattleships;
        }

        public int ArrangeShip(Square[] squares)
        {
            int result = 0;
            switch (arrangingShipsRrocess)
            {
                case ArrangingShipsRrocess.ArrangeBattleship:
                    result = ArrangeBattleShip(squares);

                    break;
                case ArrangingShipsRrocess.ArrangeCruiser:
                    result = ArrangeCruiser(squares);
                    break;
                case ArrangingShipsRrocess.ArrangeDestroyer:
                    result = ArrangeDestroyer(squares);
                    break;
                case ArrangingShipsRrocess.ArrangeSubmarine:
                    result = ArrangeSubmarine(squares);
                    break;
                case ArrangingShipsRrocess.Finished:
                    ErrorMessage = "Arranging ships was finished";
                    return -1;

            }
            if (result == -1)
            {
                ErrorMessage = shipCreator.GetErrorMessage();
            }
            return result;
        }

        #region Switch - Case for ArrangeShip 



        private int ArrangeBattleShip(Square[] squares)
        {
            if (shipCreator.IsItPossibleToCreateShip(squares, countElementsForBattleships))
            {
                Ship = shipCreator.GetShip(squares);
                currentCountBattleships++;
                if (currentCountBattleships == maxCountBattleships)
                {
                    arrangingShipsRrocess = ArrangingShipsRrocess.ArrangeCruiser;
                    CurrentMessage = "You have to arrange Cruiser: ■ ■ ■\nNow you have " + currentCountCruisers + " Cruisers from " + maxCountCruisers;
                }
                return 1;
            }
            ErrorMessage = shipCreator.GetErrorMessage();
            return -1;
        }

        private int ArrangeCruiser(Square[] squares)
        {
            if (shipCreator.IsItPossibleToCreateShip(squares, countElementsForCruisers))
            {
                Ship = shipCreator.GetShip(squares);
                currentCountCruisers++;
                CurrentMessage = "You have to arrange Cruiser: ■ ■ ■\nNow you have " + currentCountCruisers + " Cruisers from " + maxCountCruisers;
                if (currentCountCruisers == maxCountCruisers)
                {
                    arrangingShipsRrocess = ArrangingShipsRrocess.ArrangeDestroyer;
                    CurrentMessage = "You have to arrange Destroyer: ■ ■\nNow you have " + currentCountDestroyers + " Destroyers from " + maxCountDestroyers;
                }
                return 1;
            }
            ErrorMessage = shipCreator.GetErrorMessage();
            return -1;
        }

        private int ArrangeDestroyer(Square[] squares)
        {
            if (shipCreator.IsItPossibleToCreateShip(squares, countElementsForDestroyers))
            {
                Ship = shipCreator.GetShip(squares);
                currentCountDestroyers++;
                CurrentMessage = "You have to arrange Destroyer: ■ ■\nNow you have " + currentCountDestroyers + " Destroyers from " + maxCountDestroyers;

                if (currentCountDestroyers == maxCountDestroyers)
                {
                    arrangingShipsRrocess = ArrangingShipsRrocess.ArrangeSubmarine;
                    CurrentMessage = "You have to arrange Submarine: ■\nNow you have " + currentCountSubmarines + " Submarines from " + maxCountSubmarines;

                }
                return 1;
            }
            ErrorMessage = shipCreator.GetErrorMessage();
            return -1;
        }

        private int ArrangeSubmarine(Square[] squares)
        {
            if (shipCreator.IsItPossibleToCreateShip(squares, countElementsForSubmarines))
            {
                Ship = shipCreator.GetShip(squares);
                currentCountSubmarines++;
                CurrentMessage = "You have to arrange Submarine: ■\nNow you have " + currentCountSubmarines + " Submarines from " + maxCountSubmarines;
                if (currentCountSubmarines == maxCountSubmarines)
                {
                    arrangingShipsRrocess = ArrangingShipsRrocess.Finished;
                    CurrentMessage = "You have finished arranging ships!";
                }
                return 1;
            }
            ErrorMessage = shipCreator.GetErrorMessage();
            return -1;
        }


        #endregion


        private enum ArrangingShipsRrocess
        {
            ArrangeBattleship,
            ArrangeCruiser,
            ArrangeDestroyer,
            ArrangeSubmarine,
            Finished

        }
    }
}
