namespace FunWithThreads
{
    class RandomPlayer : Player
    {
        public RandomPlayer(GameManager gameMeneger, string name) :
            base(gameMeneger, name)
        {
        }

        protected override void DoOneAttempt()
        {
            int res = random.Next(40, 141);
            gameManager.ProcessValue(this, res);
        }
    }
}
