using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    interface IManager
    {
        void SetFocusToNextActivity();
        void AddActivity(Activity activity);
        void RemoveCurrentActivityFromManager();
        void RemoveAllActivitiesFromManager();

        void RemoveFocusFromThisManager();
        void HandleEvent(int eventCode);
        
    }
}
