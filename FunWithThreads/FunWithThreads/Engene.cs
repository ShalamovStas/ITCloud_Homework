using System;
using System.Collections.Generic;
using System.Threading;

namespace FunWithThreads
{
    class Engene
    {
        private AutoResetEvent autoResetEvent;
        private GameManager gameManager;
        private List<Thread> threadsList;

        public Engene()
        {
            threadsList = new List<Thread>(5);
            autoResetEvent = new AutoResetEvent(false);
        }

        public void Run()
        {
            gameManager = new GameManager(100, autoResetEvent);
            InitGame();

            autoResetEvent.WaitOne(500);
            FinishGame();
        }

        public void FinishGame()
        {
            foreach (var thread in threadsList)
                thread.Abort();

            if (gameManager.GameReport.TheVinnerHasBeenFound)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Vinner: {gameManager.GameReport.TheBestPlayer.Name}\n" +
                    $"Total amount of attempts in the game: {gameManager.GameReport.TotalAttemptsNumber}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (!gameManager.GameReport.MaxAttemptsNumberHasBeenReached)
                    Console.WriteLine("Time is up!");
                Console.WriteLine($"Vinner hasn`t been found\n" +
                    $"The best player: {gameManager.GameReport.TheBestPlayer.Name}\n" +
                    $"Total amount of attempts in the game: {gameManager.GameReport.TotalAttemptsNumber}\n" +
                    $"The  the closest value: {gameManager.GameReport.TheBestValue}");
            }
        }

        private void InitGame()
        {
            foreach (var player in new GameInitialiserHelper(gameManager).InitialiseGame())
                threadsList.Add(new Thread(player.GuessNumber));

            foreach (var thread in threadsList)
                thread.Start();
        }
    }
}