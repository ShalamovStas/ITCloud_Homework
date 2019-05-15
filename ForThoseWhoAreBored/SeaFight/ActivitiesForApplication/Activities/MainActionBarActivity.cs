using SeaFight.AbstractClasses;
using SeaFight.Common;
using SeaFight.Managers;
using SeaFight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Activities
{
    class MainActionBarActivity : Activity
    {
        private ActionBarViewModel actionBar;
        private MessageViewModel messageBar; 
        private bool ThisActivityHasFocus;
        private string[] actionBarItems;

        private bool newGameWasStarted;

        public MainActionBarActivity(IManager manager) : base(manager)
        {
            actionBarItems = new[] { "Start Game", "Save Game", "Load Game", "Exit" };
            actionBar = new ActionBarViewModel(0, 0, actionBarItems, "Main Action Bar");
            

            messageBar = new MessageViewModel(50, 0, "Main Message Bar", 50, ConsoleColor.DarkGreen);
            messageBar.PrintView();
            messageBar.ShowMessage("New game was created.\nChoose Start game");

            newGameWasStarted = false;
        }

        

        public override void Run()
        {
            ThisActivityHasFocus = true;
            actionBar.PrintView();
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
                        
                        ProcessEnterButtonPress();
                        actionBar.PrintView();
                        actionBar.ShowPointerCurrentPosition();
                        break;
                    case ConsoleKey.Tab:
                       
                        actionBar.PrintView();
                        ThisActivityHasFocus = false;
                        break;

                }
            }
            

        }

        public override void ShowActivity()
        {
            actionBar.PrintView();
        }

        private void ProcessEnterButtonPress()
        {
            switch (actionBar.ChooseItem()) 
            {
                case "Start Game":
                        newGameWasStarted = true;
                        Manager.AddActivity(new ArrangeShipsManagerActivity(Manager));
                        messageBar.ShowMessage("Game was started\nThe first step is arranging ships");
                    actionBar.ActionBarItems[0] = "New Game";
                    break;
                case "New Game":
                        Manager.RemoveAllActivitiesFromManager();
                        ThisActivityHasFocus = false;
                    break;
               
                case "Save Game":
                    messageBar.ShowMessage("Game was saved");
                    break;
                case "Load Game":
                    messageBar.ShowMessage("Game was loaded");
                    break;
                case "Exit":
                    ThisActivityHasFocus = false;
                    Manager.RemoveFocusFromThisManager();
                    
                    break;

            }
        }

        public override void RemoveActivityFromScreen()
        {
            actionBar.RemoveViewFromScreen();
            messageBar.RemoveViewFromScreen();
        }
    }
}