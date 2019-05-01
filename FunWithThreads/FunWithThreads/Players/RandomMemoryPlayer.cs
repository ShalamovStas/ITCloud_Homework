using System.Collections.Generic;

namespace FunWithThreads
{
    class RandomMemoryPlayer : Player
    {
        private List<int> thisNumbersWasUsed;

        public RandomMemoryPlayer(GameManager gameMeneger, string name) :
            base(gameMeneger, name)
        {
            thisNumbersWasUsed = new List<int>();
        }

        protected override void DoOneAttempt()
        {
            while (true)
            {
                int res = random.Next(40, 141);
                if (!thisNumbersWasUsed.Contains(res))
                {
                    thisNumbersWasUsed.Add(res);
                    gameManager.ProcessValue(this, res);
                    break;
                }
            }
        }
    }
}
