using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    class DayTenSolution
    {
        public List<string> GetInput(string inputFile)
        {
            List<string> adapters = new();
            using StreamReader sr = new(inputFile);

            while (!sr.EndOfStream)
            {
                string rule = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    adapters.Add(rule);
                }
            }
            return adapters;
        } 
        
        public List<int> ConvertInputToInt(List<string> input) => input.Select(int.Parse).ToList();

        public int GetProdOfJoltDifference(List<int> input)
        {
            input.Sort();
            int singleJoltDiff = 0;
            int threeJoltDiff = 1;
            //int deviceJolts = input.Last();
            int jolts = 0;
            foreach (var adapter in input)
            {
                int difference = adapter - jolts;
                if (difference == 1)
                {
                    ++singleJoltDiff;
                }
                else if (difference == 3)
                {
                    ++threeJoltDiff;
                }
                jolts = adapter;
            }
            
            return singleJoltDiff * threeJoltDiff;
        }

        public int GetAdapterCombos(List<int> input)
        {
            int combonations = 1;
            for (int i = 0, j = 1, k = 2, p = 3; i < input.Count - 3; i++, j++, k++, p++)
            {
                var curr = input[i];
                int difference1 = 4;
                int difference2 = 4;
                int difference3 = 4;
                
                if (j < input.Count)
                {
                    difference1 = input[j] - curr;
                }

                if (k < input.Count)
                {
                    difference2 = input[k] - curr;
                }

                if (p < input.Count)
                {
                    difference3 = input[p] - curr;
                }
                
                bool[] lessThan3 =
                {
                    difference1 <= 3,
                    difference2 <= 3,
                    difference3 <= 3
                };

                var increase = lessThan3.Count(x => x);
                Console.WriteLine(increase);

                if (increase > 1)
                {
                    combonations += increase;
                }
            }
            
            return combonations;
        }

        public Dictionary<int, List<int>> GenerateDict(List<int> input)
        {
            Dictionary<int, List<int>> connections = new();
            for (int i = 0, j = 1, k = 2, p = 3; i < input.Count; i++, j++, k++, p++)
            {
                
                var curr = input[i];
                connections.Add(curr, new());  
                
                int difference1 = 4;
                int difference2 = 4;
                int difference3 = 4;
                
                if (j < input.Count)
                    difference1 = input[j] - curr;
                if (k < input.Count)
                    difference2 = input[k] - curr;
                if (p < input.Count)
                    difference3 = input[p] - curr;

                if (difference1 <= 3)
                {
                    connections[curr].Add(input[j]);
                }
                if (difference2 <= 3)
                {
                    connections[curr].Add(input[k]);
                }
                if (difference3 <= 3)
                {
                    connections[curr].Add(input[p]);
                }
            }

            return connections;
        }

        public long GetPathsFromBegin(Dictionary<int, List<int>> connections, int start, Dictionary<int, long> memoize)
        {
            if (memoize.TryGetValue(start, out long result))
            {
                return result;
            }

            if (connections.ContainsKey(start))
            {
                long sum = 0;
                foreach (var key in connections[start])
                {
                    sum += GetPathsFromBegin(connections, key, memoize);
                }

                if (connections[start].Count == 0)
                {
                    sum = 1;
                }
                
                memoize.Add(start, sum);
                return sum;
            }
            
            return 0;
        }
        
        public long GetPaths(Dictionary<int, List<int>> revCon, int highest, Dictionary<int, long> memoize)
        {
            if (memoize.TryGetValue(highest, out long result))
            {
                return result;
            }
            if (revCon.ContainsKey(highest))
            {
                long sum = 0;
                foreach (var key in revCon[highest])
                {
                    sum += GetPaths(revCon, key, memoize);
                }
                memoize.Add(highest, sum);
                return sum;
            }
            memoize.Add(highest, 1);
            return 1;
        }
    }
}
