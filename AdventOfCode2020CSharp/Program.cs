using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "day10.txt";
            //string input = "day10_test0.txt";
            //string input = "day10_test.txt";
            DayTenSolution sol10 = new();

            var adapters = sol10.ConvertInputToInt(sol10.GetInput(input));
            
            adapters.Add(0);
            adapters.Sort();
            
            // part 1
            int product = sol10.GetProdOfJoltDifference(adapters);
            //Correct Answer in my data : 2240
            Console.WriteLine(product);
            Console.WriteLine();
           
            // part 2 
            var connections = sol10.GenerateDict(adapters);
            long betterPath3 = sol10.GetPathsFromBegin(connections, adapters[0], new());
            // The solution for part 2 in my data is: 99214346656768
            Console.WriteLine($"Better Paths: {betterPath3}");
        }
    }
}
