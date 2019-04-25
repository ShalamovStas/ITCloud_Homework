using System;

namespace BucketsAndCoinsApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //GetData(out int buckestCount, out int coinWeight, out int wrongBucketIndex);

            //double coinsWeight = GetWeight(buckestCount, coinWeight, wrongBucketIndex);
            //double index = FindBucketIndex(buckestCount, coinWeight, coinsWeight);
            //Console.ForegroundColor = ConsoleColor.DarkGreen;
            //Console.WriteLine($"Bucket index with wrong coins = {index}");

            for (int i = 0; i < 1000; i++)
            {
                if(i%55 == 0)
                    Console.WriteLine(i);
            }

            Console.ReadKey();
        }

        private static void GetData(out int buckestCount, out int coinWeight, out int wrongBucketIndex)
        {
            while (true)
            {
                Console.WriteLine("Please, type the count of buckets:");
                buckestCount = Int32.Parse(Console.ReadLine());
                Console.WriteLine($"Please, type bucket index with wrong coins, use values from [0] to [{buckestCount-1}]");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                wrongBucketIndex = Int32.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please, type coin`s weight:");
                coinWeight = Int32.Parse(Console.ReadLine());

                if (wrongBucketIndex < 0 || wrongBucketIndex >= buckestCount || buckestCount <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("Wrong values! Try again!");
                    continue;
                }
                break;
            }
        }

        private static double GetWeight(int bucketsCount, int coinWeight, int wrongBucketIndex)
        {
            double normalWeight = 0.5 * bucketsCount * (bucketsCount + 1);
            return (normalWeight - wrongBucketIndex) * coinWeight;
        }

        private static double FindBucketIndex(int bucketsCount, int coinWeight, double coinsGroupWeight)
        {
            double normalWeight = 0.5 * coinWeight * bucketsCount * (bucketsCount + 1);
            return (normalWeight - coinsGroupWeight) / coinWeight;
        }
    }
}
