using SeaFight.AbstractClasses;
using SeaFight.Activities;
using SeaFight.Logic;
using SeaFight.Session;
using SeaFight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Managers
{
    class ArrangeShipsManagerActivity : Activity, IManager
    {
        private List<Activity> activities;
        private List<Ship> ships; 
        private bool managerIsRunning;
        private int currentActivityIndex;

        private BattleshipLogic battleshipLogic;
        private List<Square> currentSquares;

        public ArrangeShipsManagerActivity(IManager manager) : base(manager)
        {
            battleshipLogic = new BattleshipLogic();
            ships = new List<Ship>();
            currentSquares = new List<Square>();
            activities = new List<Activity>
            {
                new GameBoardActivity(this, battleshipLogic, ships, currentSquares),
                new ArrangeShipsActionBarActivity(this, battleshipLogic, ships, currentSquares)
            };

            managerIsRunning = true;
            currentActivityIndex = 0;
        }

        public override void Run()
        {
            managerIsRunning = true;
            
            while (managerIsRunning)
            {
                SetFocusToNextActivity();

            }

        }

        public void AddActivity(Activity activity)
        {
            activities.Add(activity);
            activity.ShowActivity();
        }

        public void RemoveFocusFromThisManager()
        {
            managerIsRunning = false;
        }

        public void RemoveCurrentActivityFromManager()
        {
            activities.RemoveAt(currentActivityIndex);
        }

        public void SetFocusToNextActivity()
        {
            currentActivityIndex++;
            if (currentActivityIndex == activities.Count)
            {
                currentActivityIndex = 0;
            }
            activities[currentActivityIndex].Run();

        }

        public override void ShowActivity()
        {
            foreach (var item in activities)
            {
                item.ShowActivity();
            }
        }

       

        public override void RemoveActivityFromScreen()
        {
            foreach (var item in activities)
            {
                item.RemoveActivityFromScreen();
            }
        }

        public void RemoveAllActivitiesFromManager()
        {
            RemoveActivityFromScreen();
            activities.Clear();
        }

        public void HandleEvent(int eventCode)
        {
            throw new NotImplementedException();
        }
    }
}
