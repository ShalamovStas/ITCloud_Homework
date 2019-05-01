using System;
using System.Collections.Generic;
using System.Threading;

namespace FunWithThreads
{
    public class GameManager
    {
        private List<int> ThisNumbersWasUsedList;
        public int maxAttemptsNumber;
        private int currentAttemptsNumber;
        private readonly int trueValue;

        private object locker1;
        private object locker2;

        private AutoResetEvent autoResetEvent;
        public GameReport GameReport { get; private set; }

        public GameManager(int thisNumberWillBeGuessed, AutoResetEvent autoResetEvent)
        {
            this.trueValue = thisNumberWillBeGuessed;
            maxAttemptsNumber = 200;
            ThisNumbersWasUsedList = new List<int>(140);
            this.autoResetEvent = autoResetEvent;
            GameReport = new GameReport();
            locker1 = new object();
            locker2 = new object();
        }

        public bool CheckIfValueWasUsed(Player player, int value)
        {
            lock (locker2)
                return ThisNumbersWasUsedList.Contains(value);
        }

        public void ProcessValue(Player player, int value)
        {
            lock (locker1)
            {
                currentAttemptsNumber++;
                int gueesedVaue = Math.Abs(trueValue - value);
                player.SleepTime = gueesedVaue;
                ThisNumbersWasUsedList.Add(value);
                //ShowMessage(player, value);

                if (GameReport.TheBestValue > gueesedVaue)
                    CreateGameReport(false, player, gueesedVaue, false);
                if (value == trueValue)
                {
                    CreateGameReport(true, player, value, false);
                    autoResetEvent.Set();
                }
                if (currentAttemptsNumber == maxAttemptsNumber)
                {
                    CreateGameReport(false, player, value, true);
                    autoResetEvent.Set();
                }
                GameReport.TotalAttemptsNumber = currentAttemptsNumber;
            }
        }

        private void CreateGameReport(bool theVinnerHasBeenFound, Player player, int theBestValue, bool maxAttemptsNumberReached)
        {
            GameReport.TheVinnerHasBeenFound = theVinnerHasBeenFound;
            GameReport.TheBestPlayer = player;
            GameReport.TheBestValue = theBestValue;
            GameReport.TotalAttemptsNumber = currentAttemptsNumber;
            GameReport.MaxAttemptsNumberHasBeenReached = maxAttemptsNumberReached;
        }

        private void ShowMessage(Player player, int value)
        {
            Console.WriteLine($"{player.Name}: {value}");
        }
    }

    public class GameReport
    {
        public bool TheVinnerHasBeenFound { get; set; }
        public Player TheBestPlayer { get; set; }
        public int TheBestValue { get; set; }
        public int TotalAttemptsNumber { get; set; }
        public bool MaxAttemptsNumberHasBeenReached { get; set; }

        public GameReport()
        {
            TheBestValue = 140;
        }
    }
}