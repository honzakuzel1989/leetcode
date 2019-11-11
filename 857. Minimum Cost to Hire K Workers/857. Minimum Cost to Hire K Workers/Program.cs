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
            var maxWage = wage.Max();
            double qualityWithMaxWage = 0;

            for (int i = 0; i < quality.Length; i++)
            {
                if (wage[i] >= maxWage)
                {
                    qualityWithMaxWage = quality[i];
                }
            }

            var ratio = new double[quality.Length];
            for (int q = 0; q < quality.Length; q++)
            {
                // Recalculate ratio
                ratio[q] = quality[q] / qualityWithMaxWage;
            }

            var combinations = new List<(double ratio, int wage)[]>();
            for (int i = 0; i < quality.Length - 1; i++)
            {
                var res = GenerateK(i + 1, ratio, wage, K, new List<(double, int)> { (ratio[i], wage[i]) });
                combinations = combinations.Concat(res).Where(c => c.Length == K).ToList();
            }

            var prices = combinations.Select((c, i) => (index: i, price: c.Sum(x => (x.ratio * maxWage < x.wage ? x.wage : x.ratio * maxWage))));
            return prices.Min(p => p.price);
        }

        public List<(double ratio, int wage)[]> GenerateK(int curr, double[] ratio, int[] wage, int K, List<(double, int)> items)
        {
            if (items.Count == K)
                return new List<(double ratio, int wage)[]> { items.ToArray() };

            var result = new List<(double, int)[]>();
            for (int i = curr; i < wage.Length; i++)
            {
                items.Add((ratio[i], wage[i]));
                result = result.Concat(GenerateK(i + 1, ratio, wage, K, items)).ToList();
                items = new List<(double, int)> { (ratio[curr - 1], wage[curr - 1]) };
            }

            return result;
        }
    }
}
