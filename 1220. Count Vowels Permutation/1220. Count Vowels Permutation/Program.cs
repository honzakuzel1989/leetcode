#define PARALLEL
#define SERIAL

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _1220._Count_Vowels_Permutation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Use cases
            for (int i = 1; i <= 50; i++)
            {
                Console.WriteLine(new Solution().CountVowelPermutation(i));
            }

            Console.ReadLine();
        }
    }

    public class Solution
    {
        char[] vowels = new[] { 'a', 'e', 'i', 'o', 'u' };

        // Simple rule-base system.. It is easy find non-context gramar for this type system of rule
        //({S, A, E, I, O, U}, {a, e, i, o, u} P, S)
        //
        // P = {
        // S -> A | E | I | O | U
        // A -> a | aE
        // E -> e | eA | eI
        // I -> i | iA | eE | iO | iU
        // O -> o | oI | oU
        // U -> u | uA
        //
        // }
        List<IRule> rules = new List<IRule>
        {
            new ARule((f, s) => f == 'a' && s == 'e'),

            new ARule((f, s) => f == 'e' && s == 'a'),
            new ARule((f, s) => f == 'e' && s == 'i'),

            new ARule((f, s) => f == 'i' && s != 'i'),

            new ARule((f, s) => f == 'o' && s == 'i'),
            new ARule((f, s) => f == 'o' && s == 'u'),

            new ARule((f, s) => f == 'u' && s == 'a')
        };

        public uint CountVowelPermutation(int n)
        {
            return CreatePermutations(vowels, n);
        }

        private uint CreatePermutations(char[] v, int n)
        {
            Console.WriteLine($"n = {n}");

            uint sum = 0;
            Stopwatch sw = new Stopwatch();

            // Serial executions
            uint ssum = 0;
#if SERIAL            
            sw.Start();

            //Iterate over all posibilities
            foreach (var c in v)
            {
                // And create permutations
                ssum += CreatePermutations(c, v, n, 0);
            }

            sw.Stop();
            Console.WriteLine("seq: " + ssum);
            Console.WriteLine("seq: " + TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds));
            sum = ssum;
#endif

            // Parallel executions
            uint psum = 0;
#if PARALLEL
            sw.Restart();

            // Iterate over all posibilities
            Parallel.ForEach(v,
                () => (uint)0,
                (c, state, index, result) =>
                {
                    // And create permutations
                    result += CreatePermutations(c, v, n, 0);
                    return result;
                },
                result => { lock (this) psum += result; });

            sw.Stop();
            Console.WriteLine("par: " + psum);
            Console.WriteLine("par: " + TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds));
            sum = psum;
#endif

            // Check
#if PARALLEL && SERIAL
            if (ssum != psum) Console.WriteLine("Different results between sequential and parallel executions.");
#endif

            // Result
            return sum;
        }

        private uint CreatePermutations(char p, char[] v, int n, int depth)
        {
            // Increase depth
            depth++;

            // Break condition
            if (depth >= n) return 1;

            // Combination local counter
            uint counter = 0;

            // Iterate throught chars
            foreach (var c in v)
            {
                // Iterate throught rules
                foreach (var rule in rules)
                {
                    // Recursion if I can apply rule
                    if (rule.Apply(p, c))
                    {
                        // Recursion
                        counter += CreatePermutations(c, v, n, depth);
                    }
                }
            }

            // Is done for all characters and cannot apply rul
            return counter;
        }
    }

    public interface IRule
    {
        bool Apply(char first, char second);
    }

    public class ARule : IRule
    {
        private readonly Func<char, char, bool> f;

        public ARule(Func<char, char, bool> f)
        {
            this.f = f;
        }

        public bool Apply(char first, char second)
        {
            return f(first, second);
        }
    }
}
