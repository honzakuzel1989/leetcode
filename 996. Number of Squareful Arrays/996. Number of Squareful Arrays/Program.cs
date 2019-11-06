using System;
using System.Collections.Generic;
using System.Linq;

namespace _996._Number_of_Squareful_Arrays
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 1 }) == 1);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 2 }) == 0);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 1, 17, 8 }) == 2);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 1, 17, 8, 1, 17, 8 }) == 2);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 1, 17, 8, 1, 17, 8, 17}) == 2);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 1, 17, 8, 1, 17, 8, 17 }) == 2);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 2, 2, 2 }) == 1);
                Console.WriteLine(new Solution().NumSquarefulPerms(new int[] { 2, 2, 2, 2, 2, 2, 2, 2 }) == 1);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            Console.ReadLine();
        }
    }

    /// <summary>
    /// Time complexity = O(N^3 + C), where N is length of A
    /// Space complexity = O(N^2 + C), where N is length of A
    /// </summary>
    public class Solution
    {
        public int NumSquarefulPerms(int[] A)
        {
            if (!(1 <= A.Length && A.Length <= 12))
                throw new ArgumentException(nameof(A));

            // INFO: Not very effective way
            if (A.Any(x => x > 1e9))
                throw new ArgumentException(nameof(A));

            // Results
            bool[] results = new bool[A.Length];

            // Check permutation for original array
            results[0] = CheckPermutation(A);

            // Go throught the rest of array (N)
            for (int i = 1; i < A.Length; i++)
            {
                // Copy original array
                var Ai = new int[A.Length];
                Array.Copy(A, 0, Ai, 0, A.Length);

                // Make all permutations for index i
                foreach (var ai in MakePermutations(A, i, Ai))
                {
                    results[i] = CheckPermutation(ai);
                }
            }

            // Number of positive permutations
            return results.Count(x => x);
        }

        private bool CheckPermutation(int[] a)
        {
            bool CheckItemsSquereFull(int i1, int i2)
            {
                // Check if they are squerefull
                var sumSqrt = Math.Sqrt(i1 + i2);
                // INFO: (Sqrt(squarefull_pair) - Floor(squarefull_pair)) == 0
                return (sumSqrt - Math.Floor(sumSqrt) == 0);
            }

            bool CheckArraySquereFull(int[] ai)
            {
                // Check pairs of adjacent elements (N)
                for (int i = 0; i + 1 < ai.Length; i++)
                {
                    // Adjacent elements (catch one-item array)
                    var i1 = ai[i];
                    var i2 = ai[i + 1];

                    // Check
                    if (!CheckItemsSquereFull(i1, i2))
                        return false;
                }

                // Array is squerefull
                return true;
            }

            // Squarefull - different approach for arrays with single item
            return a.Length > 1 ? CheckArraySquereFull(a) : CheckItemsSquereFull(a[0], 0);
        }

        private IEnumerable<int[]> MakePermutations(int[] a, int p, int[] ap)
        {
            // Make permutation (N)
            for (int i = 0; i < a.Length; i++)
            {
                // Check - if thay are same
                if (a[p] == a[i]) continue;

                // Change position with all other items
                ap[i] = a[p];
                ap[p] = a[i];

                // Current permutation
                yield return ap;
            }
        }
    }
}