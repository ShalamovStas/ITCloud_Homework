using SeaFight.AbstractClasses;
using SeaFight.Logic;
using SeaFight.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaFight.Activities
{
    class GameBoardActivity : Activity
    {
        private bool ThisActivityHasFocus;
        private GameBoardViewModel gameBoardModel;
        private BattleshipLogic battleshipLogic;
        List<Square> currentSquares;

        public GameBoardActivity(IManager manager, BattleshipLogic battleshipLogic, List<Ship> ships, List<Square> currentSquares) : base(manager)
        {
            gameBoardModel = new GameBoardViewModel(5, 18);
            this.battleshipLogic = battleshipLogic;
            this.currentSquares = currentSquares;
        }

        

        public override void Run()
        {
            ThisActivityHasFocus = true;
            gameBoardModel.PrintView();
            gameBoardModel.MovePointerDown();
            gameBoardModel.MovePointerUp();
            while (ThisActivityHasFocus)
            {
                switch (Console.ReadKey().Key)
                {

                    case ConsoleKey.UpArrow:
                        gameBoardModel.MovePointerUp();
                        break;
                    case ConsoleKey.DownArrow:
                        gameBoardModel.MovePointerDown();
                        break;
                    case ConsoleKey.RightArrow:
                        gameBoardModel.MovePointerRight();
                        break;
                    case ConsoleKey.LeftArrow:
                        gameBoardModel.MovePointerLeft();
                        break;
                    case ConsoleKey.Enter:
                        
                        ProcessEnterButtonPress();
                        gameBoardModel.RemoveViewFromScreen();
                      
                        gameBoardModel.PrintView();
                        
                        break;
                    case ConsoleKey.Tab:
                        
                        gameBoardModel.PrintView();
                        ThisActivityHasFocus = false;
                        break;

                }
            }


        }

        public override void ShowActivity()
        {
            gameBoardModel.PrintView();
        }

        private void ProcessEnterButtonPress()
        {
            Square square = gameBoardModel.GetChoosenItem();

            if (!square.IsPartOfShip)
            {

                if (square.IsChecked)
                {
                    square.IsChecked = false;
                    currentSquares.Remove(square);
                }
                else
                {
                    square.IsChecked = true;
                    currentSquares.Add(square);
                }


                square.IsHighlightByCursor = true;
            }

        }

        public override void RemoveActivityFromScreen()
        {
            gameBoardModel.RemoveViewFromScreen();
        }


    }
}
