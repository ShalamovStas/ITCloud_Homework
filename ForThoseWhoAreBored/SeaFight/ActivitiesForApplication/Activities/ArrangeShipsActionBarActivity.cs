using SeaFight.AbstractClasses;
using SeaFight.Common;
using SeaFight.Logic;
using SeaFight.Session;
using SeaFight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Activities
{
    class ArrangeShipsActionBarActivity : Activity
    {

        private ActionBarViewModel actionBar;
        private MessageViewModel messageBarForCurrentMessages;
        private MessageViewModel statusMessageBar;
        private MessageViewModel logMessageBar;
        private bool ThisActivityHasFocus;
        private string[] actionBarItems;
        private List<Square> currentSquares;
        private List<Ship> ships;
        
        private BattleshipLogic battleshipLogic;

        public ArrangeShipsActionBarActivity(IManager manager, BattleshipLogic battleshipLogic, List<Ship> ships,
            List<Square> currentSquares) : base(manager)
        {
            actionBarItems = new[] { "Show log", "Arrange ship", "Clear board" };
            actionBar = new ActionBarViewModel(0, 10, actionBarItems, "Arranging ships action bar");
            messageBarForCurrentMessages = new MessageViewModel(50, 10, "Messages", 50, ConsoleColor.DarkYellow);
            statusMessageBar = new MessageViewModel(60, 24, "Your ships", 50, ConsoleColor.DarkYellow);
            logMessageBar = new MessageViewModel(60, 34, "Log", 50, ConsoleColor.Red);

            this.battleshipLogic = battleshipLogic;
            this.currentSquares = currentSquares;
            this.ships = ships;

        }


        public override void Run()
        {
            ThisActivityHasFocus = true;
            actionBar.PrintView();
            messageBarForCurrentMessages.ShowMessage(battleshipLogic.CurrentMessage);
            actionBar.ShowPointerCurrentPosition();

            while (ThisActivityHasFocus)
            {
                switch (Console.ReadKey().Key)
                {

                    case ConsoleKey.UpArrow:
                        actionBar.MovePointerUp();

                        break;
                    case ConsoleKey.DownArrow:
                        actionBar.MovePointerDown();
                        break;
                    case ConsoleKey.RightArrow:
                        actionBar.MovePointerDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        actionBar.MovePointerUp();
                        break;
                    case ConsoleKey.Enter:
                        actionBar.PrintView();
                        ProcessEnterButtonPress();
                        break;
                    case ConsoleKey.Tab:
                        Manager.RemoveFocusFromThisManager();
                        actionBar.PrintView();
                        ThisActivityHasFocus = false;
                        break;

                }
            }


        }

        public override void ShowActivity()
        {
            actionBar.PrintView();
            messageBarForCurrentMessages.PrintView();
            statusMessageBar.PrintView();
            logMessageBar.PrintView();
            statusMessageBar.ShowMessage(battleshipLogic.StatusTable);
            UpdateLogMessageBar();
            messageBarForCurrentMessages.ShowMessage(battleshipLogic.CurrentMessage);
        }

        private void ProcessEnterButtonPress()
        {
            switch (actionBar.ChooseItem())
            {
                case "Arrange ship":
                    ArrangeShip();
                    
                    break;
                case "Show log":
                    UpdateLogMessageBar();
                    break;

                case "Clear board":
                    foreach (var item in currentSquares)
                    {
                        item.IsChecked = false;
                    }
                    currentSquares.Clear();
                    statusMessageBar.ShowMessage(battleshipLogic.StatusTable);
                    break;
            }
        }

        private void ArrangeShip()
        {
            int result = battleshipLogic.ArrangeShip(currentSquares.ToArray());
            if (result == -1)
            {
                messageBarForCurrentMessages.ShowMessage(battleshipLogic.CurrentMessage + "\n" + battleshipLogic.ErrorMessage);
            }
            else
            {

                foreach (var item in currentSquares)
                {
                    item.IsChecked = false;
                    item.IsHighlightByCursor = false;
                    item.IsPartOfShip = true;
                }
                
                ships.Add(new Ship(currentSquares.ToArray()));
                currentSquares.Clear();
                statusMessageBar.ShowMessage(battleshipLogic.StatusTable);
                UpdateLogMessageBar();
                messageBarForCurrentMessages.ShowMessage(battleshipLogic.CurrentMessage);
            }

        }

       

        public override void RemoveActivityFromScreen()
        {
            actionBar.RemoveViewFromScreen();
            messageBarForCurrentMessages.RemoveViewFromScreen();
            statusMessageBar.RemoveViewFromScreen();
            logMessageBar.RemoveViewFromScreen();
        }



        private void UpdateLogMessageBar()
        {
            StringBuilder message = new StringBuilder();

            foreach (var item in currentSquares)
            {
                message.Append("[(" + item.X + ")(" + item.Y + ")] ");

            }
            logMessageBar.ShowMessage(message.ToString());
        }

    }

}
