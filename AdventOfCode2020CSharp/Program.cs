using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayEightSolution sol8 = new();

            //var assembly = sol8.GetInput("day8_test.txt");
            var assembly = sol8.GetInput("day8.txt");
            (int accumulator, Stack<int> operations) = sol8.RunAssembly(assembly);
           
            Console.WriteLine(assembly.Count);
            Console.WriteLine($"Accumulator is at: {accumulator}");

            sol8.FixAssembly(assembly, operations);
            (accumulator, _) = sol8.RunAssembly(assembly);
            // Correct answer: 703
            Console.WriteLine($"Accumulator: {accumulator}");
        }
    }
}
