using System;
using System.Collections.Generic;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DayThreeSolution sol3 = new();

            List<char[]> slopes = sol3.GetInput();

            Console.WriteLine("First Scenario");
            int count = sol3.SolveSlopeProblem(slopes);
            Console.WriteLine("Second Scenario");
            int count2 = sol3.SolveSlopeProblem(slopes, 1, 1);
            Console.WriteLine("Third Scenario");
            int count3 = sol3.SolveSlopeProblem(slopes, 1, 5);
            Console.WriteLine("Fourth Scenario");
            int count4 = sol3.SolveSlopeProblem(slopes, 1, 7);
            Console.WriteLine("Fifth Scenario");
            int count5 = sol3.SolveSlopeProblem(slopes, 2, 1);

            int[] collisions = { count, count2, count3, count4, count5 }; 
            
            Console.WriteLine($"my count is: {count}");
            Console.WriteLine($"my count is: {count2}");
            Console.WriteLine($"my count is: {count3}");
            Console.WriteLine($"my count is: {count4}");
            Console.WriteLine($"my count is: {count5}");

            long product = 1;

            foreach (int i in collisions)
            {
                if (i != 0)
                {
                    product *= i;
                }
            }

            Console.WriteLine("\nFirst Answer: ");
            Console.WriteLine($"my count is: {count}");
            Console.WriteLine($"The correct count is: {181}");

            Console.WriteLine("\nSecond Answer: ");
            Console.WriteLine($"The product is: {product}");
            Console.WriteLine($"The Correct Answer is: {1260601650}");
        }
    }
}
