using SeaFight.AbstractClasses;
using SeaFight.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.ViewModels
{
    class MessageViewModel : MessageView

    {
        public MessageViewModel(int cursorStartPosition_X, int cursorStartPosition_Y, string title, int width, ConsoleColor textColor) : base(cursorStartPosition_X, cursorStartPosition_Y, title, width, textColor)
        {
           
        }

        
    }
}
