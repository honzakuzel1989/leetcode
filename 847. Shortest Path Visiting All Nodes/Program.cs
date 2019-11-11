using System;
using System.Linq;
using System.Collections.Generic;

namespace _847._Shortest_Path_Visiting_All_Nodes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Solution.Shortest(new int[][]{new int[]{1,2,3}, new int[]{0}, new int[]{0}, new int [] {0}}));
            Console.WriteLine(Solution.Shortest(new int[][]{new int[]{0}}));
            Console.WriteLine(Solution.Shortest(new int[][]{new int[]{1}, new int[]{2}, new int[]{0}}));
        }
    }

    class Solution
    {
        public static int Shortest(int [][] graph)
        {
            int [] paths = new int[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                System.Console.WriteLine($"\n->[{i}]");
                paths[i] = Shortest(graph, i, graph[i], new Dictionary<int, List<int>>());
                System.Console.WriteLine($"=>[{paths[i]}]");
            }

            return paths.Min();
        }

        public static int Shortest(int [][] graph, int curr, int[] node, Dictionary<int, List<int>> visited)
        {
            if(!visited.ContainsKey(curr))
                visited[curr] = new List<int>();

            if(visited.Keys.Count == graph.Length)
                return 0;

            int hops = 0;
            foreach (var i in node)
            {
                if(!visited[curr].Contains(i))
                {
                    System.Console.WriteLine($"-> {i}");

                    visited[curr].Add(i);
                    hops = 1 + Shortest(graph, i, graph[i], visited);
                }
            }

            return hops;
        }
    }
}   