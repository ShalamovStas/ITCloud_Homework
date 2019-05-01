namespace FunWithThreads
{
    class OrderPlayer : Player
    {
        public int currentNumber;

        public OrderPlayer(GameManager gameMeneger, string name) :
            base(gameMeneger, name)
        {
            currentNumber = 39;
        }

        protected override void DoOneAttempt()
        {
            gameManager.ProcessValue(this, ++currentNumber);
        }
    }
}
