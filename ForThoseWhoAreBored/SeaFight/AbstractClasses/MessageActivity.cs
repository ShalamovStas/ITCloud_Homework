using SeaFight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaFight.AbstractClasses
{
    abstract class MessageActivity : Activity
    {
 
        public string  Title { get; set; }

        public MessageActivity(IManager manager, string title) : base(manager)
        {
            Title = title;
        }

        public sealed override void Run() { }

        public abstract void ShowMessage(string message);
        

    }
}
