using System;
using ITCloudAcademy.CoinsProblem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITCloudAcademy.Tests
{
    [TestClass]
    public class CoinsSolverTests
    {
        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(1, 2)]
        [DataRow(5, 2)]
        [DataRow(10, 2)]
        [DataRow(3, 2)]
        [DataRow(1, 358)]
        [DataRow(8, 358)]
        [DataRow(4, 358)]
        [DataRow(10, 358)]
        [DataRow(1, 1000)]
        public void GetFakeCoinsIndex_ShouldReturnRightIndex(int wrongBucketIndex, int coinWeight)
        {
            int solution = CoinsSolver.GetFakeCoinsIndex((coinsCountArray) => GenerateWeight(wrongBucketIndex, coinWeight, coinsCountArray));

            Assert.AreEqual(wrongBucketIndex, solution);
        }

        private int GenerateWeight(int wrongBucketIndex, int coinWeight, int[] coinsCountInEachBucket)
        {
            if (coinsCountInEachBucket?.Length != 10)
                throw new NotSupportedException($"Hey! Your array MUST contain exactly 10 elements. But actually contains {coinsCountInEachBucket?.Length}.");

            int weight = 0;
            for (int i = 0; i < 10; i++)
            {
                int coinsCount = coinsCountInEachBucket[i];
                if (coinsCount < 0)
                    throw new NotSupportedException($"Hey! coins count cannot be less than 0. Got {coinsCount} from you.");

                if (i + 1 == wrongBucketIndex)
                    weight += (coinWeight - 1) * coinsCount;
                else
                    weight += coinWeight * coinsCount;
            }

            return weight;
        }
    }
}
