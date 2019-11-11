using System;
using System.Collections.Generic;
using System.Linq;

namespace _857._Minimum_Cost_to_Hire_K_Workers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Solution().MincostToHireWorkers(new int[] { 10, 20, 5 }, new int[] { 70, 50, 30 }, 2));
            Console.WriteLine(new Solution().MincostToHireWorkers(new int[] { 10, 20, 5 }, new int[] { 70, 50, 30 }, 3));

            Console.WriteLine(new Solution().MincostToHireWorkers(new int[] { 3, 1, 10, 10, 1 }, new int[] { 4, 8, 2, 2, 7 }, 3));

            Console.ReadLine();
        }
    }

    public class Solution
    {
        public double MincostToHireWorkers(int[] quality, int[] wage, int K)
        {
            var workerGroups = new List<int[]>();
            for (int i = 0; i < quality.Length - 1; i++)
            {
                var res = GenerateWorkersGroups(i + 1, wage.Length, K, new List<int> { i });
                workerGroups = workerGroups.Concat(res).Where(c => c.Length == K).ToList();
            }

            var prices = new List<double>();
            foreach (var workerGroup in workerGroups)
            {
                var maxWage = 0;
                var qualityWithMaxWage = 0;

                for (int i = 0; i < workerGroup.Length; i++)
                {
                    if (wage[workerGroup[i]] > maxWage)
                    {
                        maxWage = wage[workerGroup[i]];
                        qualityWithMaxWage = quality[workerGroup[i]];
                    }
                }

                var ratio = new double[quality.Length];

                for (int i = 0; i < quality.Length; i++)
                {
                    ratio[i] = quality[i] / (double)qualityWithMaxWage;
                }

                var price = workerGroup.Sum(w => ratio[w] * maxWage < wage[w] ? wage[w] : ratio[w] * maxWage);
                prices.Add(price);
            }

            return prices.Min();
        }

        public List<int[]> GenerateWorkersGroups(int curr, int numOfWorkes, int K, List<int> workers)
        {
            if (workers.Count == K)
                return new List<int[]> { workers.ToArray() };

            var result = new List<int[]>();
            for (int i = curr; i < numOfWorkes; i++)
            {
                workers.Add(i);
                result = result.Concat(GenerateWorkersGroups(i + 1, numOfWorkes, K, workers)).ToList();
                workers = new List<int> { curr - 1 };
            }

            return result;
        }
    }
}
