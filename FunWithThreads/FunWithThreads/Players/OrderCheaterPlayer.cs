namespace FunWithThreads
{
    class OrderCheaterPlayer : Player
    {
        private int currentNumber;

        public OrderCheaterPlayer(GameManager gameMeneger, string name) :
            base(gameMeneger, name)
        {
            currentNumber = 39;
        }

        protected override void DoOneAttempt()
        {
            while (true)
            {
                if (!gameManager.CheckIfValueWasUsed(this, ++currentNumber))
                {
                    gameManager.ProcessValue(this, currentNumber);
                    break;
                }
            }
        }
    }
}
