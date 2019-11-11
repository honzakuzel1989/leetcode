using System;

namespace _214._Shortest_Palindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Solution().ShortestPalindrome("abcd"));
            Console.WriteLine(new Solution().ShortestPalindrome("a"));
            Console.WriteLine(new Solution().ShortestPalindrome("aacecaaa"));
            Console.WriteLine(new Solution().ShortestPalindrome("aaccecaaa"));
            Console.WriteLine(new Solution().ShortestPalindrome("drom  mordnilap"));
            Console.WriteLine(new Solution().ShortestPalindrome("x = x * 2"));
            Console.WriteLine(new Solution().ShortestPalindrome(""));
        }
    }

    class Solution 
    {
        public string ShortestPalindrome(string s) 
        {
            int idx = 1;

            // Find the biggest palindrome in text from back
            for (int i = s.Length - 1; i > idx; i--)
            {
                string part = s.Substring(0, i);
                if(CheckPalindrome(part))
                {
                    idx = i;
                    break;
                }
            }

            // Construct new palindrome from a front
            int originalLen = s.Length;
            for (int i = idx, cnt = 0; i < originalLen; i++, cnt++)
            {
                // Naive way.. It is possible to use array or spans or whatever
                s = s.Insert(0, s[i + cnt].ToString());
            }

            // New palindrome
            return s;
        }

        private bool CheckPalindrome(string s)
        {
            // Check palindrome - it has to have same parts
            for (int i = 0; i < s.Length / 2.0; i++)
            {
                if(s[i] != s[s.Length - 1 - i])
                    return false;
            }

            return true;
        }
    }
}
