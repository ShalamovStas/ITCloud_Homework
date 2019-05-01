using System;
using System.Collections.Generic;

namespace FunWithThreads
{
    class GameInitialiserHelper
    {
        private List<Player> playersList;
        private GameManager gameManager;

        public GameInitialiserHelper(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        
        public List<Player> InitialiseGame()
        {
            playersList = new List<Player>();

            Console.WriteLine("Please, type the count of players: (minValue = 2; maxValue = 8)");
            int playersCount = Int32.Parse(Console.ReadLine());

            for (int i = 0; i < playersCount; i++)
            {
                Console.Write("Player name:\t");
                string playerName = Console.ReadLine();
                Console.WriteLine("Choose playerType:\n" +
                    "randomPlayer[0]\norderPlayer[1]\nrandomMemoryPlayer[2]\norderCheaterPlayer[3]\n" +
                    "randomCheaterPlayer[4]");
                int playerType = Int32.Parse(Console.ReadLine());
                CreatePlayer(playerType, playerName);

            }
            Console.Clear();
            return playersList;
        }

        private void CreatePlayer(int playerType, string playerName)
        {
            switch (playerType)
            {
                case 0:
                    playersList.Add(new RandomPlayer(gameManager, playerName));
                    break;
                case 1:
                    playersList.Add(new OrderPlayer(gameManager, playerName));
                    break;
                case 2:
                    playersList.Add(new RandomMemoryPlayer(gameManager, playerName));
                    break;
                case 3:
                    playersList.Add(new OrderCheaterPlayer(gameManager, playerName));
                    break;
                case 4:
                    playersList.Add(new RandomCheaterPlayer(gameManager, playerName));
                    break;
            }
        }
    }
}
