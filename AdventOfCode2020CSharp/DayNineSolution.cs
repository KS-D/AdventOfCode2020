using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2020CSharp
{
    class DayNineSolution
    {
        public List<string> GetInput(string inputFile)
        {
            List<string> cypher = new();
            using StreamReader sr = new(inputFile);

            while (!sr.EndOfStream)
            {
                string rule = sr.ReadLine();
                if (!string.IsNullOrWhiteSpace(rule))
                {
                    cypher.Add(rule);
                }
            }
            return cypher;
        }

        public List<long> ConvertInputToInt(List<string> input) => input.Select(s => long.Parse(s)).ToList();

        public Dictionary<long, List<long>> CalculatePotentialSums(long[] cypher)
        {
            Dictionary<long, List<long>> sums = new();
            for (int i = 0; i < cypher.Length; i++)
            {
                sums.Add(cypher[i], new());
                for (int j = i + 1; j < cypher.Length; j++)
                {
                    sums[cypher[i]].Add(cypher[i] + cypher[j]);
                }
            }

            return sums;
        }

        public HashSet<long> UniqueSums(Dictionary<long, List<long>> sums) => 
                new HashSet<long>(sums.Values.SelectMany(s => s));

        public int FindInvalidIndex(string inputFile, int preamble)
        {
            var input = GetInput(inputFile);
            var parsedInput = ConvertInputToInt(input);

            var potentialSums = CalculatePotentialSums(parsedInput.GetRange(0, preamble).ToArray());

            for (int i = preamble, j = 0; i < parsedInput.Count; i++, j++)
            {
                var uniqueSums = UniqueSums(potentialSums);

                if (!uniqueSums.Contains(parsedInput[i]))
                {
                    return i;
                }
                
                UpdateSum(potentialSums, parsedInput[j], parsedInput[i]);
            }   

            return -1;
        }

        private void UpdateSum(Dictionary<long, List<long>> sums, long remove, long add)
        {
            sums.Remove(remove);
            foreach (var pair in sums)
            {
                pair.Value.Add(pair.Key + add);
            }
            sums.Add(add, new());
        }

        public long ConstructInvalidData(List<long> range, long invalidData)
        {
            for (int i = 0; i < range.Count; i++)
            {
                long sum = range[i], smallest = range[i], largest = range[i];
                
                for (int j = i + 1; j < range.Count; j++)
                {
                    long curr = range[j];

                    if (smallest > curr)
                    {
                        smallest = curr;
                    }

                    if (largest < curr)
                    {
                        largest = curr;
                    }
                    
                    sum += curr;
                    
                    if (sum > invalidData)
                    {
                        break;
                    }
                    
                    if (sum == invalidData)
                    {
                        return smallest + largest;
                    }
                }
            }

            return -1;
        }
    }
}
