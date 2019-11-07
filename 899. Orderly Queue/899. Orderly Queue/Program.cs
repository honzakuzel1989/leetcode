using System;
using System.Collections.Generic;
using System.Linq;

namespace _899._Orderly_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Solution().OrderlyQueue("cba", 1) == "acb");
            Console.WriteLine(new Solution().OrderlyQueue("baaca", 3) == "aaabc");
            Console.WriteLine(new Solution().OrderlyQueue("abcabc", 2) == "aabcbc");

            Console.ReadLine();
        }
    }

    public class Solution
    {
        public string OrderlyQueue(string S, int K)
        {
            if (!(1 <= K && K <= S.Length && S.Length <= 1000))
                throw new ArgumentException();

            // First tail and head - not very effective vay - I can use array or slice or something
            string head = new string(S.Take(K).ToArray());
            string tail = new string(S.Skip(K).Take(S.Length - K).ToArray());

            // Until some character in tail is bigger then characters in head
            // M
            while (head.Any(h => tail.Any(t => h > t)))
            {
                // Find index of local max in K-part of S
                int imax = 0;
                for (int i = 1; i < K; i++)
                {
                    // Local maximum
                    if (S[imax] < S[i]) imax = i;
                }

                // Reorder string
                char[] s = new char[S.Length];
                // N
                for (int i = 0; i < S.Length; i++)
                {
                    if (i < imax) s[i] = S[i];
                    if (i > imax) s[i - 1] = S[i];
                }

                // Move max to the end
                s[s.Length - 1] = S[imax];

                // And create new one
                S = new string(s);

                // Get new tail and head
                head = new string(S.Take(K).ToArray());
                tail = new string(S.Skip(K).Take(S.Length - K).ToArray());
            }

            // Reduced string, where is not bigger item in tail than in head
            // INFO: it is possible to order head and tail for lexicographically smallest result
            return S;
        }
    }
}
