using SeaFight.AbstractClasses;
using SeaFight.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.Managers
{
    class MainManagerActivity : IManager
    {
        private List<Activity> activities;
       
        private bool managerIsRunning;
        private int currentActivityIndex;

        public MainManagerActivity()
        {
            activities = new List<Activity>
            {
                new MainActionBarActivity(this),

            };

          

            managerIsRunning = true;
            currentActivityIndex = 0;
        }

        public void Run()
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

       
        public void RemoveAllActivitiesFromManager()
        {
            foreach (var item in activities)
            {
                item.RemoveActivityFromScreen();
            }

            activities.Clear();
            currentActivityIndex = 0;
            activities.Add(new MainActionBarActivity(this));
            
        }

        public void HandleEvent(int eventCode)
        {
            throw new NotImplementedException();
        }
    }
}
