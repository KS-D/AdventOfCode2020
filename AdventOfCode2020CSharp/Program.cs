using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "day9_test.txt";
            string input = "day9.txt";
            DayNineSolution sol9 = new();

            List<long> cypher = sol9.ConvertInputToInt(sol9.GetInput(input));

            int invalidIndex = sol9.FindInvalidIndex(input, 25);
            //int invalidIndex = sol9.FindInvalidIndex("day9.txt", 25);
            // Correct Answer: 400480901
            Console.WriteLine($"Invalid: {cypher[invalidIndex]}");

            long encryptionWeakness = sol9.ConstructInvalidData(cypher.GetRange(0, invalidIndex), cypher[invalidIndex]);
            // Correct Answer: 67587168
            Console.WriteLine($"encryption weakness: {encryptionWeakness}");

        }
    }
}
