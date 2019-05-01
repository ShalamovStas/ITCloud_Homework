namespace FunWithThreads
{
    class RandomCheaterPlayer : Player
    {
        public RandomCheaterPlayer(GameManager gameMeneger, string name) :
            base(gameMeneger, name)
        {
        }

        protected override void DoOneAttempt()
        {
            while (true)
            {
                int res = random.Next(40, 141);
                if (!gameManager.CheckIfValueWasUsed(this, res))
                {
                    gameManager.ProcessValue(this, res);
                    break;
                }
            }
        }
    }
}
