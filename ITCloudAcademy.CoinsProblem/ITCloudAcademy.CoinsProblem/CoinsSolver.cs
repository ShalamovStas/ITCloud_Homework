using System;
using System.Collections.Generic;
using System.IO;

namespace ITCloudAcademy.CoinsProblem
{
    public delegate int AskWeightsDelegate(int[] howManyCoinsFromEachBucketToPutOnWeights);

    public class CoinsSolver
    {
        public static int GetFakeCoinsIndex(AskWeightsDelegate askWeightsDelegate)
        {
            // Example of how to call weights. You may do it only once. Otherwise you will get an exception
            // Array MUST contain 10 elements!
            int weight = askWeightsDelegate(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}); // Take 1 coin from the first bucket, 2 coins from the 2nd bucket and so on.

            
            // You need to return the right index of the bucket.
            // Indexes of buckets start from 1. NOT from 0!
            return FindBucketIndex(weight);
        }

        private static int FindBucketIndex(int weight)
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    if ((weight + i + 1) % 55 == 0)
            //        return i + 1;
            //}
            //throw new InvalidDataException();

            return (int)Math.Round((double)weight / 55) * 55 - weight; ;
        }
    }
}
