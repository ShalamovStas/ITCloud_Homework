using System;
using System.Threading;

namespace FunWithThreads
{
    public abstract class Player
    {
        public string Name { get; private set; }
        protected Random random;
        protected GameManager gameManager;
        public int SleepTime { get; set; }

        public Player(GameManager gameMeneger, string name)
        {
            this.gameManager = gameMeneger;
            Name = name;
            random = new Random();
        }

        protected abstract void DoOneAttempt();

        public void GuessNumber()
        {
            while(true)
            {
                Thread.Sleep(SleepTime);
                if (gameManager.GameReport.TheVinnerHasBeenFound)
                    break;
                DoOneAttempt();
            }
        }

    }
}
