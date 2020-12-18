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
            //sol16.Parse("day16_part2test.txt");
            sol16.Parse("day16.txt");
            var validRange = sol16.FindRange();
            foreach (var i in validRange)
            {
                Console.WriteLine(i);
            }
            // 25961 is the correct answer
            Console.WriteLine(sol16.ErrorRate(validRange));
            
            // first move discard invalid tickets, update ErrorRate to remove incorrect tickets
            
            var positions = sol16.FindValidFieldPosition();
            positions = sol16.Solve(positions);
            while (sol16.ContainsDuplicatePositions(positions))
            {
                positions = sol16.Solve(positions);
            }

            Console.WriteLine(sol16.GetDepartureProduct(positions));

        }
    }
}
