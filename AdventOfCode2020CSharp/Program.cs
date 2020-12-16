using System;
using System.Diagnostics;

namespace AdventOfCode2020CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            DaySixteenSolution sol16 = new();
            //sol16.Parse("day16_test.txt");
            sol16.Parse("day16.txt");
            var validRange = sol16.FindRange();
            foreach (var i in validRange)
            {
                Console.WriteLine(i);
            }
            
            // 25961 is the correct answer
            Console.WriteLine(sol16.ErrorRate(validRange));
        }
    }
}
